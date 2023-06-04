/* eslint-disable @typescript-eslint/naming-convention */
import { InjectionToken } from "@angular/core";

export const YM_CONFIG: InjectionToken<unknown> = new InjectionToken("ngx-metrika Config");

export interface IMetrikaConfig {
    id: number;
    trackPageViews?: boolean;
    webvisor?: boolean;
    triggerEvent?: boolean;
    defer?: boolean;
    clickmap?: boolean;
    trackLinks?: boolean;
    accurateTrackBounce?: boolean;
    PlayerId?: number;
}
