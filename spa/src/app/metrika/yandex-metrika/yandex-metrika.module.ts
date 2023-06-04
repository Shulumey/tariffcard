import { ModuleWithProviders, NgModule } from "@angular/core";
import { IMetrikaConfig, YM_CONFIG } from "./interfaces";

@NgModule()
export class YandexMetrikaModule {
    public static forRoot(
        config?: IMetrikaConfig
    ): ModuleWithProviders<YandexMetrikaModule> {
        return {
            ngModule: YandexMetrikaModule,
            providers: [
                { provide: YM_CONFIG, useValue: config }
            ]
        };
    }
}
