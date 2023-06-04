import {Component, ChangeDetectionStrategy, Input, TemplateRef} from "@angular/core";

@Component({
    selector: "tc-header-message",
    templateUrl: "./header-message.component.html",
    styleUrls: ["./header-message.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderMessageComponent {

    @Input()
    public messageTemplate: TemplateRef<any>;
}
