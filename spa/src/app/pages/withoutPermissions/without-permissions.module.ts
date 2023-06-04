import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { WithoutPermissionsRoutingModule } from "./without-permissions-routing.module";
import { ComponentsModule } from "../../components";
import { WithoutPermissionsPageComponent } from "./without-permissions-page/without-permissions-page.component";


@NgModule({
    declarations: [
        WithoutPermissionsPageComponent
    ],
    exports: [
        WithoutPermissionsPageComponent
    ],
    imports: [
        CommonModule,
        WithoutPermissionsRoutingModule,
        ComponentsModule
    ]
})
export class WithoutPermissionsModule { }
