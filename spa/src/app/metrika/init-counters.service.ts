import { Injectable } from "@angular/core";
import { NmUserModel } from "nm-widgets";
import { GoogleTagManagerService } from "./google-tag-manager/google-tag-manager.service";
import { YandexMetrikaService } from "./yandex-metrika/yandex-metrika.service";

@Injectable({
    providedIn: "root"
})
export class InitCounters {
    constructor(
        private googleTagManagerService: GoogleTagManagerService,
        private yandexMetrikaService: YandexMetrikaService
    ) { }

    public init(user?: NmUserModel | null): void {
        if (user) {
            this.googleTagManagerService.init(user.id);
        } else {
            this.googleTagManagerService.init();
        }
        this.yandexMetrikaService.init();
    }
}
