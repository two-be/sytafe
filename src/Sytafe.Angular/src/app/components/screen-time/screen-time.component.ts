import { Component, Input, OnInit } from "@angular/core"
import * as dayjs from "dayjs"

import { AppService } from "src/app/app.service"
import { DayOfWeek, ScreenTimeInfo, SelectOptionInfo } from "src/app/models"
import { AbstractComponent } from "../abstract.component"

@Component({
    selector: "screen-time",
    templateUrl: "./screen-time.component.html"
})
export class ScreenTimeComponent extends AbstractComponent implements OnInit {

    @Input()
    userId = ""

    dayOfWeeks: DayOfWeek[] = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]
    dayOfWeekOptions: SelectOptionInfo[] = []
    selectedDayOfWeeks: DayOfWeek[] = []
    screenTime = new ScreenTimeInfo()
    screenTimes: ScreenTimeInfo[] = []

    constructor(service: AppService) {
        super(service)
    }

    delete(value: ScreenTimeInfo) {
        try {
            if (!confirm("Are you sure?")) {
                return
            }
        } catch (err) {
            this.error(err)
        }
    }

    edit(value: ScreenTimeInfo) {
        try {
            let { ...screenTime } = value
            screenTime.availableFrom = dayjs(`0001-01-01 ${screenTime.availableFrom}`).toDate()
            screenTime.availableTo = dayjs(`0001-01-01 ${screenTime.availableTo}`).toDate()
            this.screenTime = screenTime
            this.selectedDayOfWeeks = [screenTime.dayOfWeek]
            this.setDisplay(true)
        } catch (err) {
            this.error(err)
        }
    }

    getDisplayMinute() {
        if (this.screenTime.minuteLimit == 720) {
            return "no limit"
        }
        return `${this.screenTime.minuteLimit} min`
    }

    initDayOfWeekOptions() {
        this.dayOfWeekOptions = this.dayOfWeeks.map(x => ({ label: x, value: x }))
    }

    async initScreenTimes() {
        try {
            if (this.userId) {
                let rs = await this.service.get.screenTimesByUser(this.userId)
                let screenTimes = this.dayOfWeeks.map(x => {
                    let data = rs.find(y => y.dayOfWeek == x) || new ScreenTimeInfo()
                    data.dayOfWeek = x
                    return data
                })
                this.screenTimes = screenTimes
            }
        } catch (err) {
            this.error(err)
        }
    }

    async ngOnInit() {
        this.setProcessing(true)
        this.initDayOfWeekOptions()
        await this.initScreenTimes()
        this.setProcessing(false)
    }

    async save() {
        try {
            this.setProcessing(true)
            let screenTimes = this.selectedDayOfWeeks.map(x => {
                let data = new ScreenTimeInfo()
                data.anytime = this.screenTime.anytime
                data.availableFrom = this.screenTime.availableFrom
                data.availableTo = this.screenTime.availableTo
                data.dayOfWeek = x
                data.minuteLimit = this.screenTime.minuteLimit
                data.userId = this.userId
                return data
            })
            await this.service.post.screenTimes(screenTimes)
            this.setDisplay(false)
            await this.initScreenTimes()
            this.setProcessing(false)
        } catch (err) {
            this.error(err)
        }
    }
}