import { Injectable, NgZone } from "@angular/core";
import { NmUserModel } from "nm-widgets";
import { BehaviorSubject, from, Observable } from "rxjs";
import { filter, map, switchMap, take } from "rxjs/operators";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";

@UntilDestroy()
@Injectable({
    providedIn: "root"
})
export class AuthService {
    private userSub = new BehaviorSubject<NmUserModel | null | undefined>(undefined);
    public get user$(): Observable<NmUserModel | null> {
        return this.userSub.pipe(filter((x: NmUserModel) => x !== undefined), map((x: NmUserModel | null) => x as NmUserModel | null));
    }

    public login(): void {
        window.nmWidgetsLoaded().then(() => window.authService.login());
    }

    public get isAuthenticated(): Observable<boolean> {
        return this.user$
            .pipe(
                untilDestroyed(this),
                take(1),
                map((x: NmUserModel | null) => !!x)
            );
    }

    public get authorizationHeaderValue(): Observable<string | null> {
        return this.isAuthenticated
            .pipe(
                untilDestroyed(this),
                map((x: boolean) => x ? `${window.authService.tokenType} ${window.authService.accessToken}` : null)
            );
    }

    constructor(
        private ngZone: NgZone
    ) {
        from(window.nmWidgetsLoaded())
            .pipe(switchMap(() => window.authService.user$))
            .subscribe({
                next: (user: NmUserModel | null) => ngZone.run(() => {
                    this.userSub.next(user);
                })
            });
    }
}
