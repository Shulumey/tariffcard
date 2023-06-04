import { Component, Input, TemplateRef, ViewEncapsulation } from "@angular/core";

@Component({
    selector: "tcTooltip",
    encapsulation: ViewEncapsulation.None,
    styleUrls: ["./tooltip.component.scss"],
    templateUrl: "./tooltip.component.html"
})
export class TooltipComponent {
    @Input()
    public set content(value: string | TemplateRef<any>) {
        if (value && typeof (value) !== "string") {
            this.contentTemplate = value;
        } else {
            this.contentValue = value as string;
        }
    }

    public contentValue: string;
    public contentTemplate: TemplateRef<any>;
}
