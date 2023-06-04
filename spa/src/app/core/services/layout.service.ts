import { Injectable } from "@angular/core";
import { SnackMessage } from "../models";
import { BehaviorSubject, Observable, Subject, timer } from "rxjs";
import { debounce, debounceTime, distinctUntilChanged, take } from "rxjs/operators";

const LOADED_DEFAULT: boolean = false;

@Injectable({
    providedIn: "root"
})
export class LayoutService {

    private _messages$: Subject<SnackMessage> = new Subject<SnackMessage>();

    private _modalMessages$: Subject<string> = new Subject<string>();

    private _progress$: BehaviorSubject<boolean | number> = new BehaviorSubject<boolean | number>(false);

    private _loaded$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(LOADED_DEFAULT);

    public static scrollTop(): void {
        const supportsNativeSmoothScroll: boolean = "scrollBehavior" in document.documentElement.style;
        const element: Element = document.scrollingElement || document.documentElement;

        if (supportsNativeSmoothScroll) {
            window.scrollTo({ top: 0, behavior: "smooth" });
        } else {
            element.scrollTop = 0;
        }
    }

    public get progress$(): Observable<boolean | number> {
        return this._progress$.pipe(distinctUntilChanged());
    }

    public get messages$(): Observable<SnackMessage> {
        return this._messages$.pipe();
    }

    public get modalMessages$(): Observable<string> {
        return this._modalMessages$.pipe();
    }

    public get loaded$(): Observable<boolean> {
        return this._loaded$.pipe(
            distinctUntilChanged(),
            debounce((loaded: boolean) => {
                // load with debounce | unload immediately
                const milliseconds: number = loaded ? 200 : 0;
                return timer(milliseconds);
            })
        );
    }

    public showInfo(text: string): void {
        const message: SnackMessage = {
            duration: 3000,
            severity: "info",
            text
        };
        this._messages$.next(message);
    }

    public showWarning(text: string): void {
        const message: SnackMessage = {
            duration: 10000,
            severity: "warn",
            text
        };
        this._messages$.next(message);
    }

    public showError(text: string): void {
        const message: SnackMessage = {
            duration: 60000,
            severity: "error",
            text
        };
        this._messages$.next(message);
    }

    public showErrorModal(data: string): void {
        this._modalMessages$.next(data);
    }

    public loadEnd(): void {
        this._loaded$.next(true);
    }

    public showProgress(percentage: number = 0): void {
        this._progress$.next(percentage || true);
    }

    public hideProgress(): void {
        this._progress$.next(false);

        // debounce switch loaded$ to true
        // check once after 100 ms, and switch only when progress is still false
        this._progress$.pipe(
            debounceTime(100),
            take(1)
        ).subscribe((x: boolean | number) => {
            // 'if' instead 'filter' pipline operation for automate unsubscription by 'take'
            if (x === false) {
                this._loaded$.next(true);
            }
        });
    }
}
