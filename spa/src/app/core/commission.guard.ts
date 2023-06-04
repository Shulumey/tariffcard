import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { Observable } from "rxjs";
import { map, tap } from "rxjs/operators";
import { AuthService } from "./services/auth.service";
import { PERMISSION_NAME, SPA_URL } from "./constants";
import { NmRightName } from "nm-widgets/auth.service";
import { NmUserModel } from "nm-widgets";

@Injectable({
    providedIn: "root"
})
export class CommissionGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private _router: Router
    ) { }

    public canActivate(): Observable<boolean> {
        return this.authService.user$.pipe(
            map((x: NmUserModel) =>
                !!x && !!x.permission && x.permission.findIndex((p: NmRightName) => p === PERMISSION_NAME.VIEW_COMMISSION) > -1
            ),
            tap((x: boolean) => {
                if (!x) {
                    this._router.navigate([SPA_URL.WITHOUT_PERMISSION]);
                }
            })
        );
    }
}
