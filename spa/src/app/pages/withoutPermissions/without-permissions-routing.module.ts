import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { WithoutPermissionsPageComponent } from "./without-permissions-page/without-permissions-page.component";

const routes: Routes = [
    {
        path: "",
        component: WithoutPermissionsPageComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WithoutPermissionsRoutingModule { }
