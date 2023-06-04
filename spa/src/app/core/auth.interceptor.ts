import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "./services/auth.service";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";
import { switchMap } from "rxjs/operators";

@UntilDestroy()
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }

    public intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        return this.authService.authorizationHeaderValue
            .pipe(
                untilDestroyed(this),
                switchMap((x: string | null) => {
                    if (x === null) {
                        return next.handle(request);
                    }

                    const authReq: HttpRequest<unknown> = request.clone({
                        headers: request.headers.set("Authorization", x)
                    });

                    return next.handle(authReq);
                })
            );
    }
}
