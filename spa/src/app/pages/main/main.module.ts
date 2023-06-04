import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MainRoutingModule } from "./main-routing.module";
import { MainPageComponent } from "./main-page/main-page.component";
import { ComponentsModule } from "../../components";


@NgModule({
    declarations: [
        MainPageComponent
    ],
    exports: [
        MainPageComponent
    ],
    imports: [
        CommonModule,
        MainRoutingModule,
        ComponentsModule
    ]
})
export class MainModule { }
