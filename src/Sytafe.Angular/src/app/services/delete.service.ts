import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom, Subject, takeUntil } from "rxjs";
import { getApi } from "../utilities";

@Injectable()
export class DeleteService {

    unsubscribe = new Subject()

    constructor(private http: HttpClient) { }

    destroy() {
        this.unsubscribe.next({})
        this.unsubscribe.complete()
    }

    user(id: string) {
        this.unsubscribe.next({})
        return firstValueFrom(this.http.delete(getApi("user", id)).pipe(takeUntil(this.unsubscribe)))
    }
}