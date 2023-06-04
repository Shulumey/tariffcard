import { Directive, Input, TemplateRef, HostListener, ViewContainerRef, ComponentRef, ElementRef } from "@angular/core";
import {
    ConnectedOverlayPositionChange,
    ConnectionPositionPair,
    FlexibleConnectedPositionStrategy,
    Overlay,
    OverlayRef,
    ScrollStrategyOptions
} from "@angular/cdk/overlay";
import { ComponentPortal } from "@angular/cdk/portal";
import { TooltipComponent } from "./tooltip.component";
import { HorizontalAttachment, Position, VerticalAttachment } from "./tooltip";
import { OriginConnectionPosition, OverlayConnectionPosition } from "@angular/cdk/overlay";
import { IConnectionPosition } from "./models/connection-position.model";
import { ParametrizedConnectionPosition } from "./models/parametrized-connection-position.model";

@Directive({
    selector: "[tcTooltip]"
})
export class TooltipDirective {
    @Input("tcTooltip")
    public content: string | TemplateRef<any>;

    @Input("tcTooltipDisabled")
    public disabled: boolean = false;

    @Input("tcTooltipPosition")
    public position: Position = "top";

    @Input("tcTooltipHorizontalAttachment")
    public horizontalAttachment: HorizontalAttachment = "center";

    @Input("tcTooltipVerticalAttachment")
    public verticalAttachment: VerticalAttachment = "center";

    @Input("tcTooltipCssClass")
    public cssClass: string;

    constructor(private viewContainerRef: ViewContainerRef, private overlay: Overlay, private scrollStrategy: ScrollStrategyOptions) {
    }

    private overlayRef: OverlayRef | undefined;
    private tooltipComponentRef: ComponentRef<TooltipComponent>;
    private positionOffsetX: number = 10;
    private positionOffsetY: number = 10;

    private static tooltipDirectives: TooltipDirective[] = [];

    @HostListener("focusin")
    @HostListener("mouseenter")
    public show(): void {
        if (this.disabled || !this.content) {
            return;
        }

        this.clearHideTimer();
        if (!this.overlayRef) {
            const anchorElement: ElementRef<any> = this.viewContainerRef.element;

            const connectionPosition: IConnectionPosition = this.getConnectionPosition(this.position, this.horizontalAttachment, this.verticalAttachment);
            const fallbackPositions: ConnectionPositionPair[] =
                connectionPosition.fallbackPositions.map((fp: "top" | "bottom" | "left" | "right") => this
                    .getConnectionPosition(fp, this.horizontalAttachment, this.verticalAttachment)
                    .position);
            const positions: ConnectionPositionPair[] = [connectionPosition.position].concat(fallbackPositions);

            const positionStrategy: FlexibleConnectedPositionStrategy = this.overlay.position().flexibleConnectedTo(anchorElement)
                .withFlexibleDimensions(false)
                .withPositions(positions);

            let panelClass: string[] = ["tc-tooltip"];
            const positionClassPrefix: string = `${panelClass[0]}-`;

            if (this.cssClass) {
                panelClass = panelClass.concat(this.cssClass.split(" "));
            }

            this.overlayRef = this.overlay.create({
                positionStrategy,
                panelClass,
                scrollStrategy: this.scrollStrategy.reposition()
            });

            positionStrategy.positionChanges.subscribe((change: ConnectedOverlayPositionChange) => {
                if (this.overlayRef) {
                    this.overlayRef.overlayElement.className.split(" ").forEach((className: string) => {
                        if (className.indexOf(positionClassPrefix) === 0 && this.overlayRef && !panelClass.find((x: string) => x === className)) {
                            this.overlayRef.overlayElement.classList.remove(className);
                        }
                    });
                    const parametrizedConnectionPosition: ParametrizedConnectionPosition = change.connectionPair as ParametrizedConnectionPosition;
                    this.overlayRef.overlayElement.classList.add(`${positionClassPrefix}${parametrizedConnectionPosition.position}`);
                }
            });

            this.overlayRef.overlayElement.onmouseenter = (): void => {
                this.clearHideTimer();
            };

            this.overlayRef.overlayElement.onmouseleave = (): void => {
                this.checkHide();
            };

            this.tooltipComponentRef = this.overlayRef.attach(new ComponentPortal(TooltipComponent));
            this.tooltipComponentRef.instance.content = this.content;

            this.hideAllShowedTooltips(false);

            TooltipDirective.tooltipDirectives.push(this);
        }
    }

    private hideTimeout = 100;

    private hideTimer?: NodeJS.Timeout;
    @HostListener("focusout")
    @HostListener("mouseleave")
    public checkHide(): void {
        this.clearHideTimer();
        this.hideTimer = setTimeout(() => {
            this.hide();
        }, this.hideTimeout);
    }

    private clearHideTimer(): void {
        if (this.hideTimer) {
            clearTimeout(this.hideTimer);
            this.hideTimer = undefined;
        }
    }

    public hide(): void {
        if (this.tooltipComponentRef) {
            this.tooltipComponentRef.destroy();
        }
        if (this.overlayRef) {
            this.overlayRef.detach();
            this.overlayRef.dispose();
            this.overlayRef = undefined;
        }
    }

    @HostListener("mousedown")
    public hideAllShowedTooltips(includeSelf: boolean = true): void {
        if (TooltipDirective.tooltipDirectives.length) {
            for (const tooltip of TooltipDirective.tooltipDirectives) {
                if (includeSelf || tooltip !== this) {
                    tooltip.hide();
                    if (!tooltip.disabled) {
                        // Preventing instant showing
                        tooltip.disabled = true;
                        setTimeout(() => tooltip.disabled = false);
                    }
                }
            }
            TooltipDirective.tooltipDirectives = [];
        }
    }

    private getConnectionPosition(position: Position, horizontalAttachment: HorizontalAttachment, verticalAttachment: VerticalAttachment): IConnectionPosition {
        let fallbackPositions: Position[] = [];
        let offsetX: number | undefined;
        let offsetY: number | undefined;
        const res: [OriginConnectionPosition, OverlayConnectionPosition] = [{ originX: "center", originY: "center" }, { overlayX: "center", overlayY: "center" }];
        switch (position) {
            case "top":
                res[0].originY = "top";
                res[1].overlayY = "bottom";
                res[0].originX = horizontalAttachment;
                res[1].overlayX = "center";
                fallbackPositions = ["bottom", "right", "left"];
                offsetY = -this.positionOffsetY;
                break;
            case "right":
                res[0].originX = "end";
                res[1].overlayX = "start";
                res[0].originY = verticalAttachment;
                res[1].overlayY = "center";
                fallbackPositions = ["left", "top", "bottom"];
                offsetX = this.positionOffsetX;
                break;
            case "bottom":
                res[0].originY = "bottom";
                res[1].overlayY = "top";
                res[0].originX = horizontalAttachment;
                res[1].overlayX = "center";
                fallbackPositions = ["top", "right", "left"];
                offsetY = this.positionOffsetY;
                break;
            case "left":
                res[0].originX = "start";
                res[1].overlayX = "end";
                res[0].originY = verticalAttachment;
                res[1].overlayY = "center";
                fallbackPositions = ["right", "top", "bottom"];
                offsetX = -this.positionOffsetX;
                break;
        }
        return {
            position: new ParametrizedConnectionPosition({
                origin: res[0],
                overlay: res[1],
                position,
                horizontalAttachment,
                verticalAttachment,
                offsetX,
                offsetY
            }),
            fallbackPositions
        };
    }
}
