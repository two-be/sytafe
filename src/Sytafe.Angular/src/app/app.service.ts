import { Injectable } from "@angular/core"
import { Subject } from "rxjs"
import { DeleteService } from "./services/delete.service"

import { GetService } from "./services/get.service"
import { PostService } from "./services/post.service"
import { PutService } from "./services/put.service"

@Injectable()
export class AppService {

    delete: DeleteService

    constructor(public get: GetService, public post: PostService, public put: PutService, del: DeleteService) {
        this.delete = del
    }

    destroy() {
        this.get.destroy()
        this.post.destroy()
        this.put.destroy()
        this.delete.destroy()
    }

    init() {
        this.get.unsubscribe = new Subject()
        this.post.unsubscribe = new Subject()
        this.put.unsubscribe = new Subject()
        this.delete.unsubscribe = new Subject()
    }
}