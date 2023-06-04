import { Injectable } from "@angular/core";
import { CanActivate } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "./services/auth.service";
import { tap } from "rxjs/operators";

@Injectable({
    providedIn: "root"
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService) { }

    public canActivate(): Observable<boolean> {
        return this.authService.isAuthenticated
            .pipe(
                tap((x: boolean) => {
                    if (!x) {
                        this.authService.login();
                    }
                }));
    }
}
