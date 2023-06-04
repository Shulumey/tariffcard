import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { BusyLoaderComponent } from "./busy-loader/busy-loader.component";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatDialogModule } from "@angular/material/dialog";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { CommonDialogComponent } from "./common-dialog/common-dialog.component";


@NgModule({
    declarations: [
        BusyLoaderComponent,
        CommonDialogComponent
    ],
    imports: [
        CommonModule,
        MatSnackBarModule,
        MatDialogModule,
        MatIconModule,
        MatButtonModule
    ],
    exports: [
        BusyLoaderComponent,
        CommonDialogComponent
    ]
})
export class BusyModule { }
