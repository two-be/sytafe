import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { firstValueFrom, Subject, takeUntil } from "rxjs"

import { ScreenTimeInfo, UsedInfo, UserInfo } from "../models"
import { getApi } from "../utilities"

@Injectable()
export class GetService {

    unsubscribe = new Subject()

    constructor(private http: HttpClient) { }

    destroy() {
        this.unsubscribe.next({})
        this.unsubscribe.complete()
    }

    screenTimesByUser(userId: string) {
        return firstValueFrom(this.http.get<ScreenTimeInfo[]>(getApi("screenTime", `user/${userId}`)).pipe(takeUntil(this.unsubscribe)))
    }

    usedsByUser(userId: string) {
        return firstValueFrom(this.http.get<UsedInfo[]>(getApi("used", `user/${userId}`)).pipe(takeUntil(this.unsubscribe)))
    }

    user(id: string) {
        return firstValueFrom(this.http.get<UserInfo>(getApi("user", id)).pipe(takeUntil(this.unsubscribe)))
    }

    users() {
        return firstValueFrom(this.http.get<UserInfo[]>(getApi("user")).pipe(takeUntil(this.unsubscribe)))
    }
}