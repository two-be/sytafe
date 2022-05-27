import { HttpClientModule } from "@angular/common/http"
import { NgModule } from "@angular/core"
import { FormsModule } from "@angular/forms"
import { BrowserModule } from "@angular/platform-browser"
import { BrowserAnimationsModule } from "@angular/platform-browser/animations"

import { AppRoutingModule } from "./app-routing.module"
import { AppComponent } from "./app.component"
import { AppService } from "./app.service"
import { components } from "./components"
import { httpInterceptorProviders } from "./http-interceptors"
import { modules } from "./modules"
import { pages } from "./pages"
import { services } from "./services"

@NgModule({
  declarations: [
    AppComponent,
    ...components,
    ...pages,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ...modules,
  ],
  providers: [AppService, httpInterceptorProviders, ...services],
  bootstrap: [AppComponent]
})
export class AppModule { }
