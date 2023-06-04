import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LOCALE_ID, NgModule } from "@angular/core";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./components/app/app.component";
import { MetrikaModule } from "./metrika";
import { MAT_DATE_LOCALE } from "@angular/material/core";
import { AuthInterceptor } from "./core";
import { BusyModule } from "./busy/busy.module";
import { APP_BASE_HREF, registerLocaleData } from "@angular/common";

const BASE_HREF: string = window.location.pathname.indexOf("/tariffcard") === 0 ? "/tariffcard/" : "/";

import localeRu from "@angular/common/locales/ru";
registerLocaleData(localeRu);

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        AppRoutingModule,
        MetrikaModule,
        BusyModule
    ],
    providers: [
        { provide: APP_BASE_HREF, useValue: BASE_HREF },
        { provide: MAT_DATE_LOCALE, useValue: "ru-RU" },
        { provide: LOCALE_ID, useValue: "ru" },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
