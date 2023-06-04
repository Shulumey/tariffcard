/* eslint-disable @typescript-eslint/naming-convention */
import { Component, OnInit, ChangeDetectionStrategy, Renderer2 } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { BaseNgDestroy, LayoutService, getMessageDuration, SnackMessage } from "../../core";
import { CommonDialogComponent } from "../common-dialog/common-dialog.component";

const BUSY_BODY_CLASS: string = "tc_busy";

@Component({
    selector: "tc-busy-loader",
    templateUrl: "./busy-loader.component.html",
    styleUrls: ["./busy-loader.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class BusyLoaderComponent extends BaseNgDestroy implements OnInit {
    // TODO: circle component
    public readonly CIRCLE_SIZE = 32;
    public readonly CIRCLE_VIEW_BOX = `0 0 ${this.CIRCLE_SIZE} ${this.CIRCLE_SIZE}`;
    public readonly CIRCLE_STROKE_WIDTH = 4;
    public readonly CIRCLE_RADIUS = this.CIRCLE_SIZE / 2 - this.CIRCLE_STROKE_WIDTH / 2;
    public readonly CIRCLE_DASHARRAY = Math.PI * 2 * this.CIRCLE_RADIUS;
    public readonly CIRCLE_OFFSET = this.CIRCLE_SIZE / 2;
    public readonly CIRCLE_DASHOFFSET = (this.CIRCLE_DASHARRAY - (this.CIRCLE_DASHARRAY / 100) * 75);

    constructor(
        private snackBar: MatSnackBar,
        public dialog: MatDialog,
        private layoutService: LayoutService,
        private renderer: Renderer2
    ) {
        super();
    }

    public ngOnInit(): void {
        this.subs = [
            this.layoutService.progress$.subscribe(
                (progress: boolean | number) => {
                    if (progress) {
                        this.renderer.addClass(document.body, BUSY_BODY_CLASS);
                    } else {
                        this.renderer.removeClass(document.body, BUSY_BODY_CLASS);
                    }
                },
                this.onError
            ),
            this.layoutService.messages$.subscribe(
                ({ text, duration, severity }: SnackMessage) => {
                    this.snackBar.open(text, "Закрыть", {
                        duration: duration ? duration : getMessageDuration(severity),
                        horizontalPosition: "center",
                        verticalPosition: "bottom",
                        panelClass: ["tc-snack-bar-container", `tc-snack-bar-container_severity-${severity}`]
                    });
                },
                this.onError
            ),
            this.layoutService.modalMessages$.subscribe((data: string) => {
                this.dialog.open(CommonDialogComponent, {
                    data,
                    closeOnNavigation: true,
                    disableClose: true,
                    panelClass: ["tc-dialog", "tc-dialog_compact-warning"]
                });
            })
        ];
    }

    private onError(err: any): void {
        console.warn(err);
    }

}
