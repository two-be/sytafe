import { Component } from "@angular/core";
import { AppService } from "src/app/app.service";

import { AbstractComponent } from "src/app/components"

@Component({
    templateUrl: "./home.page.html"
})
export class HomePage extends AbstractComponent {

    constructor(service: AppService) {
        super(service)
    }
}