import { Component, OnDestroy } from "@angular/core"
import { AppService } from "../app.service"
import { UserInfo } from "../models"
import { getUser, log } from "../utilities"

@Component({
    template: ""
})
export class AbstractComponent implements OnDestroy {

    display = false
    ngModelOptions = { standalone: true }
    processing = false
    userInfo = new UserInfo()

    constructor(public service: AppService) {
        this.init()
    }

    error(err) {
        if (err.error && err.error.message) {
            alert(err.error.message)
        } else {
            alert(err.message)
        }
        console.error(err)
        this.setProcessing(false)
    }

    init() {
        this.service.init()
        this.initUserInfo()
    }

    initUserInfo() {
        let user = getUser()
        this.setUserInfo(user)
    }

    log(value) {
        log(value)
    }

    ngOnDestroy() {
        this.service.destroy()
    }

    setDisplay(value: boolean) {
        this.display = value
    }

    setProcessing(value: boolean) {
        this.processing = value
    }

    setUserInfo(user: UserInfo) {
        let json = JSON.stringify(user)
        localStorage.setItem("user", json)
        this.userInfo = user
    }
}