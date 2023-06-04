/* eslint-disable @typescript-eslint/naming-convention */
import { Inject, Injectable, Renderer2, RendererFactory2 } from "@angular/core";
import { filter, skip } from "rxjs/operators";
import { ICommonOptions, IMetrikaGoalEventOptions, IMetrikaHitEventOptions, IMetrikaHitOptions } from "../interfaces";
import { MetrikaService } from "../metrika.service";
import { IMetrikaConfig, YM_CONFIG } from "./interfaces";

declare let Ya: any;

@Injectable({
    providedIn: "root"
})
export class YandexMetrikaService {
    private readonly _config: IMetrikaConfig;
    private _renderer: Renderer2;
    private _isAdFoxCreated: boolean = false;

    constructor(
        private metrikaService: MetrikaService,
        @Inject(YM_CONFIG) ymConfig: IMetrikaConfig,
        rendererFactory: RendererFactory2
    ) {
        this._renderer = rendererFactory.createRenderer(null, null);
        this._config = Object.assign({ triggerEvent: true, trackPageViews: true }, ymConfig);
    }

    private static getCounterNameById(id: string | number): string {
        return `yaCounter${id}`;
    }

    private static getCounterById(id: any): any {
        return window[YandexMetrikaService.getCounterNameById(id)];
    }

    private configure(): void {
        YandexMetrikaService.insertMetrika(this._config);
        this.checkCounter(this._config.id)
            .then(() => {
                if (this._config.trackPageViews) {
                    this.metrikaService.hit.subscribe((y: IMetrikaHitEventOptions) => {
                        this.onHit(y.url, y.hitOptions);
                    });
                    this.metrikaService.hitUrl();
                }
                this.metrikaService.reachGoal$.pipe(skip(1), filter((y: IMetrikaGoalEventOptions) => !!y.target))
                    .subscribe((y: IMetrikaGoalEventOptions) => {
                        this.onReachGoal(y.target, y.options);
                    });
            });
    }

    public init(): void {
        if (this._config.id) {
            this.configure();
        } else {
            // eslint-disable-next-line no-console
            console.debug("this._config.id is empty");
        }
    }

    private onHit(url: string, options?: IMetrikaHitOptions): void {
        try {
            const ya: any = YandexMetrikaService.getCounterById(this._config.id);
            if (typeof ya !== "undefined") {
                ya.hit(url, options);
            } else {
                console.warn("ya is undefined");
            }
        } catch (err) {
            console.error("Yandex Metrika hit error", err);
        }
    }

    public onReachGoal(type: string, options: ICommonOptions = {}): void {
        try {
            const ya: any = YandexMetrikaService.getCounterById(this._config.id);
            if (typeof ya !== "undefined") {
                const opt: IMetrikaGoalEventOptions = {
                    target: type,
                    options: {
                        params: options.params,
                        callback: options.callback,
                        ctx: options.ctx
                    }
                };
                ya.reachGoal(opt);
            } else {
                console.warn("ya is undefined");
            }
        } catch (error) {
            console.error("error", error);
            console.warn(`'Event with type [${type}] can\'t be fired because counter is still loading'`);
        }
    }

    public createAdFox(): void {
        if (this._isAdFoxCreated) {
            return;
        }
        Ya.adfoxCode.create({
            ownerId: 169942,
            containerId: "adfox_161364041789458882",
            params: {
                p1: "cogll",
                p2: "hcqb"
            }
        });
        this._isAdFoxCreated = true;
    }

    private static insertMetrika(config: IMetrikaConfig): string {
        const name: string = "yandex_metrika_callbacks2";
        window[name] = window[name] || [];
        window[name].push(() => {
            try {
                const a: string = YandexMetrikaService.getCounterNameById(config.id);
                window[a] = new Ya.Metrika2(config);
            } catch (e) {
                // ignore
            }
        });

        const n: HTMLScriptElement = document.getElementsByTagName("script")[0];
        const s: HTMLScriptElement = document.createElement("script");
        s.type = "text/javascript";
        s.async = true;
        s.src = "https://mc.yandex.ru/metrika/tag.js";
        const insertScriptTag: () => ((Node & ParentNode) | null) = (): Node & ParentNode | null => n.parentNode && n.parentNode.insertBefore(s, n);

        if ((window as any).opera === "[object Opera]") {
            document.addEventListener("DOMContentLoaded", insertScriptTag, false);
        } else {
            insertScriptTag();
        }

        return name;
    }

    private checkCounter(id: string | number): Promise<any> {
        const counterName: string = `yacounter${id}inited`;
        return new Promise((resolve: (value: any) => void, reject: (reason?: any) => void): void => {
            this._renderer.listen("document", counterName, () => {
                resolve({});
            });
        });
    }
}
