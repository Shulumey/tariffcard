import { Component, ChangeDetectionStrategy, Inject } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
    selector: "tc-common-dialog",
    templateUrl: "./common-dialog.component.html",
    styleUrls: ["./common-dialog.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CommonDialogComponent {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: string
    ) { }

}
