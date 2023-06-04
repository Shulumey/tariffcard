import { EventEmitter, Injectable } from "@angular/core";
import { Event, NavigationEnd, Router } from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { filter } from "rxjs/operators";
import { IMetrikaGoalEventOptions, IMetrikaHitEventOptions } from "./interfaces";

@Injectable({
    providedIn: "root"
})
export class MetrikaService {
    public reachGoal$ = new BehaviorSubject<IMetrikaGoalEventOptions>({ target: "test" });

    public hit = new EventEmitter<IMetrikaHitEventOptions>();

    private _previousUrl: string;

    constructor(
        private _router: Router
    ) {
        this.initMetriks();
    }

    public reachGoal(goalOptions: IMetrikaGoalEventOptions): void {
        this.reachGoal$.next(goalOptions);
    }

    private initMetriks(): void {
        this._router.events.pipe(
            filter((event: Event) => event instanceof NavigationEnd)
        ).subscribe(() => this.hitUrl());
    }

    public hitUrl(): void {
        const options: IMetrikaHitEventOptions = {
            url: window.location.href,
            hitOptions: {
                referer: this._previousUrl
            }
        };
        this.hit.emit(options);
        this._previousUrl = window.location.href;
    }
}
