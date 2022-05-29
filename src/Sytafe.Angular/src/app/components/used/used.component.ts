import { Component, Input, OnInit } from "@angular/core";
import { AppService } from "src/app/app.service";
import { UsedInfo } from "src/app/models";

import { AbstractComponent } from "../abstract.component"

@Component({
    selector: "used",
    templateUrl: "./used.component.html"
})
export class UsedComponent extends AbstractComponent implements OnInit {

    @Input()
    userId = ""

    useds: UsedInfo[] = []

    constructor(service: AppService) {
        super(service)
    }

    async initUseds() {
        try {
            this.setProcessing(true)
            this.useds = await this.service.get.usedsByUser(this.userId)
            this.setProcessing(false)
        } catch (err) {
            this.error(err)
        }
    }

    async ngOnInit() {
        await this.initUseds()
    }
}