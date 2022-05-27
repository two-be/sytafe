import { Injectable } from "@angular/core"
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from "@angular/common/http"
import { Observable } from "rxjs"

import { getUser } from "../utilities"

@Injectable()
export class NoopInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authReq = req.clone({ setHeaders: { Authorization: `Bearer ${getUser().token}` } })
        return next.handle(authReq)
    }
}