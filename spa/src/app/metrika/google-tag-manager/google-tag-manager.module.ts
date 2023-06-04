import { NgModule, ModuleWithProviders } from "@angular/core";
import { IGoogleTagManagerConfig } from "./interfaces";

@NgModule()
export class GoogleTagManagerModule {
    public static forRoot(
        config: IGoogleTagManagerConfig
    ): ModuleWithProviders<GoogleTagManagerModule> {
        return {
            ngModule: GoogleTagManagerModule,
            providers: [{ provide: "googleTagManagerConfig", useValue: config }]
        };
    }
}
