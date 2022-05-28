import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AppService } from "src/app/app.service";

import { AbstractComponent } from "src/app/components"
import { UserInfo } from "src/app/models";

@Component({
    templateUrl: "./user.page.html"
})
export class UserPage extends AbstractComponent implements OnInit {

    user = new UserInfo()

    constructor(service: AppService, private route: ActivatedRoute, private router: Router) {
        super(service)
    }

    async initUser(id) {
        try {
            this.setProcessing(true)
            this.user = await this.service.get.user(id)
            this.setProcessing(false)
        } catch (err) {
            this.error(err)
        }
    }

    async ngOnInit() {
        if (!this.userInfo.isAdministrator) {
            this.router.navigateByUrl("/home")
            return
        }
        let userId = this.route.snapshot.params["id"]
        await this.initUser(userId)
    }
}