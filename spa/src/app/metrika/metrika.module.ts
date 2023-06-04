import { ModuleWithProviders, NgModule } from "@angular/core";
import { environment } from "src/environments/environment";
import { GoogleTagManagerModule } from "./google-tag-manager/google-tag-manager.module";
import { GoogleTagManagerService } from "./google-tag-manager/google-tag-manager.service";
import { MetrikaGoalDirective } from "./metrika-goal.directive";
import { MetrikaService } from "./metrika.service";
import { YandexMetrikaModule } from "./yandex-metrika/yandex-metrika.module";

@NgModule({
    declarations: [MetrikaGoalDirective],
    exports: [MetrikaGoalDirective],
    imports: [
        YandexMetrikaModule.forRoot({
            id: environment.yaCounterId,
            defer: true,
            clickmap: true,
            trackLinks: true,
            accurateTrackBounce: true,
            webvisor: true
        }),
        GoogleTagManagerModule.forRoot({
            id: environment.googleTagManagerId
        })
    ]
})
export class MetrikaModule {
    public static forRoot(): ModuleWithProviders<MetrikaModule> {
        return {
            ngModule: MetrikaModule,
            providers: [
                MetrikaService,
                GoogleTagManagerService
            ]
        };
    }
}
