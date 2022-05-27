import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { firstValueFrom, Subject, takeUntil } from "rxjs"
import * as dayjs from "dayjs"

import { ScreenTimeInfo, UserInfo } from "../models"
import { getApi } from "../utilities"

@Injectable()
export class PostService {

    unsubscribe = new Subject()

    constructor(private http: HttpClient) { }

    destroy() {
        this.unsubscribe.next({})
        this.unsubscribe.complete()
    }

    screenTimes(body: ScreenTimeInfo[]) {
        body.forEach(x => {
            x.availableFrom = x.anytime ? "00:00:00" : dayjs(x.availableFrom).format("HH:mm:00")
            x.availableTo = x.anytime ? "23:59:59" : dayjs(x.availableTo).format("HH:mm:59")
        })
        this.unsubscribe.next({})
        return firstValueFrom(this.http.post<ScreenTimeInfo[]>(getApi("screenTime"), body).pipe(takeUntil(this.unsubscribe)))
    }

    user(body: UserInfo) {
        this.unsubscribe.next({})
        return firstValueFrom(this.http.post<UserInfo>(getApi("user"), body).pipe(takeUntil(this.unsubscribe)))
    }

    userForSignIn(body: UserInfo) {
        this.unsubscribe.next({})
        return firstValueFrom(this.http.post<UserInfo>(getApi("user", "sign-in"), body).pipe(takeUntil(this.unsubscribe)))
    }
}