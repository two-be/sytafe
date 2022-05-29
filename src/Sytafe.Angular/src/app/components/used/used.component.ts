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

    minute = "0"
    useds: UsedInfo[] = []

    constructor(service: AppService) {
        super(service)
    }

    async initMinutes() {
        try {
            this.minute = await this.service.get.usedsForTodayForMinuteByUser(this.userId)
        } catch (err) {
            this.error(err)
        }
    }

    async initUseds() {
        try {
            this.useds = await this.service.get.usedsByUser(this.userId)
        } catch (err) {
            this.error(err)
        }
    }

    async ngOnInit() {
        this.setProcessing(true)
        await this.initUseds()
        await this.initMinutes()
        this.setProcessing(false)
    }
}