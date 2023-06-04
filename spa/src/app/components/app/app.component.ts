import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { InitCounters } from "../../metrika/init-counters.service";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit {
    public title = "tariff-card-spa";

    constructor(router: Router,
                private initCounters: InitCounters) {
        window.nmWidgetsLoaded().then(() => window.nmMetricsService.initialize(router));
    }

    public ngOnInit(): void {
        this.initCounters.init();
    }
}
