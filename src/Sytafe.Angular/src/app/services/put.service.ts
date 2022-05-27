import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom, Subject, takeUntil } from "rxjs";
import { UserInfo } from "../models";
import { getApi } from "../utilities";

@Injectable()
export class PutService {

    unsubscribe = new Subject()

    constructor(private http: HttpClient) { }

    destroy() {
        this.unsubscribe.next({})
        this.unsubscribe.complete()
    }

    user(body: UserInfo) {
        this.unsubscribe.next({})
        return firstValueFrom(this.http.put(getApi("user", body.id), body).pipe(takeUntil(this.unsubscribe)))
    }
}