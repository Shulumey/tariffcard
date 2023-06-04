import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./core";
import { CommissionGuard } from "./core";
import { SPA_URL } from "./core";
import { WithoutPermissionsModule } from "./pages/withoutPermissions/without-permissions.module";

class MainModule {
}

const routes: Routes = [
    {
        path: "",
        canActivate: [CommissionGuard],
        // eslint-disable-next-line @typescript-eslint/explicit-function-return-type, @typescript-eslint/naming-convention
        loadChildren: () => import("./pages/main/main.module").then((m: { MainModule: MainModule }) => m.MainModule)
    },
    {
        path: SPA_URL.WITHOUT_PERMISSION,
        canActivate: [AuthGuard],
        // eslint-disable-next-line @typescript-eslint/explicit-function-return-type
        loadChildren: () =>
            import("./pages/withoutPermissions/without-permissions.module")
                // eslint-disable-next-line @typescript-eslint/naming-convention
                .then((m: { WithoutPermissionsModule: WithoutPermissionsModule }) => m.WithoutPermissionsModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
