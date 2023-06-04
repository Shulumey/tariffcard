/* eslint-disable @typescript-eslint/naming-convention */
import { Inject, Injectable, Optional } from "@angular/core";
import { filter, skip } from "rxjs/operators";
import { IMetrikaGoalEventOptions, IMetrikaHitEventOptions } from "../interfaces";
import { MetrikaService } from "../metrika.service";
import { IGoogleTagManagerConfig } from "./interfaces";

@Injectable({
    providedIn: "root"
})
export class GoogleTagManagerService {
    private isLoaded = false;

    private browserGlobals = {
        windowRef: (): any => window,
        documentRef: (): Document => document
    };

    constructor(
        @Optional()
        @Inject("googleTagManagerConfig")
        public config: IGoogleTagManagerConfig = { id: null },
        @Optional() @Inject("googleTagManagerId") public googleTagManagerId: string,
        @Optional()
        @Inject("googleTagManagerAuth")
        public googleTagManagerAuth: string,
        @Optional()
        @Inject("googleTagManagerPreview")
        public googleTagManagerPreview: string,
        private metrikaService: MetrikaService
    ) {
        if (this.config === null) {
            this.config = { id: null };
        }

        this.config = {
            ...this.config,
            id: googleTagManagerId || this.config.id,
            gtm_auth: googleTagManagerAuth || this.config.gtm_auth,
            gtm_preview: googleTagManagerPreview || this.config.gtm_preview
        };

        if (this.config.id === null) {
            throw new Error("Google tag manager ID not provided.");
        }
    }

    public getDataLayer(): any {
        const window: any = this.browserGlobals.windowRef();
        window.dataLayer = window.dataLayer || [];
        return window.dataLayer;
    }

    private pushOnDataLayer(obj: object): void {
        const dataLayer: any = this.getDataLayer();
        dataLayer.push(obj);
    }

    public addGtmToDom(): void {
        if (this.isLoaded) {
            return;
        }
        const doc: Document = this.browserGlobals.documentRef();
        this.pushOnDataLayer({
            "gtm.start": new Date().getTime(),
            "event": "gtm.js"
        });

        const gtmScript: HTMLScriptElement = doc.createElement("script");
        gtmScript.id = "GTMscript";
        gtmScript.async = true;
        gtmScript.src = this.applyGtmQueryParams(
            "https://www.googletagmanager.com/gtm.js"
        );
        doc.head.insertBefore(gtmScript, doc.head.firstChild);

        const gtmIframe: HTMLIFrameElement = doc.createElement("iframe");
        gtmIframe.src = `https://www.googletagmanager.com/ns.html?id=${this.config.id}`;
        gtmIframe.height = "0";
        gtmIframe.width = "0";
        gtmIframe.setAttribute("style", "display:none;visibility:hidden");
        doc.body.insertBefore(gtmIframe, doc.body.firstChild);

        this.isLoaded = true;
    }

    public pushTag(item: object): void {
        if (!this.isLoaded) {
            this.addGtmToDom();
        }
        this.pushOnDataLayer(item);
    }

    private applyGtmQueryParams(url: string): string {
        if (url.indexOf("?") === -1) {
            url += "?";
        }

        return (
            url +
            Object.keys(this.config)
                .filter((k: string) => this.config[k])
                .map((k: string) => `${k}=${this.config[k]}`)
                .join("&")
        );
    }

    public init(userId?: number): void {
        this.metrikaService.hit.subscribe((y: IMetrikaHitEventOptions) => {
            this.onHit(y.url, userId);
        });
        this.metrikaService.hitUrl();

        this.metrikaService.reachGoal$.pipe(skip(1), filter((y: IMetrikaGoalEventOptions) => !!y.target))
            .subscribe((y: IMetrikaGoalEventOptions) => {
                this.onReachGoal(y.target);
            });
    }

    private onHit(url: string, userId: number): void {
        if (this.config.id !== "0" && url) {
            const gtmTag: { PlayerID: number; event: string; pageName: string } = {
                event: "page",
                pageName: url,
                PlayerID: userId
            };

            this.pushTag(gtmTag);
        }
    }

    // eslint-disable-next-line @typescript-eslint/no-empty-function
    public onReachGoal(type: string): void {
    }
}
