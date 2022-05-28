import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AppService } from "src/app/app.service";

import { AbstractComponent } from "src/app/components"
import { UserInfo } from "src/app/models"

@Component({
    templateUrl: "./users.page.html"
})
export class UsersPage extends AbstractComponent implements OnInit {

    user = new UserInfo()
    users: UserInfo[] = []

    constructor(service: AppService, private router: Router) {
        super(service)
    }

    async initUsers() {
        try {
            this.setProcessing(true)
            this.users = await this.service.get.users()
            this.setProcessing(false)
        } catch (err) {
            this.error(err)
        }
    }

    add() {
        try {
            this.user = new UserInfo()
            this.setDisplay(true)
        } catch (err) {
            this.error(err)
        }
    }

    async delete(value: UserInfo) {
        try {
            if (!confirm("Are you sure?")) {
                return
            }
            this.setProcessing(true)
            await this.service.delete.user(value.id)
            this.users = this.users.filter(x => x.id != value.id)
            this.setProcessing(false)
        } catch (err) {
            this.error(err)
        }
    }

    edit(value: UserInfo) {
        try {
            let { ...user } = value
            this.user = user
            this.setDisplay(true)
        } catch (err) {
            this.error(err)
        }
    }

    async ngOnInit() {
        if (!this.userInfo.isAdministrator) {
            this.router.navigateByUrl("/home")
            return
        }
        await this.initUsers()
    }

    async save() {
        try {
            this.setProcessing(true)
            let id = this.user.id
            if (id) {
                let rs = await this.service.put.user(this.user)
                let user = this.users.find(x => x.id == id)
                Object.keys(rs).forEach(x => user[x] = rs[x])
            } else {
                let rs = await this.service.post.user(this.user)
                this.users = [rs, ...this.users]
            }
            this.setDisplay(false)
            this.setProcessing(false)
        } catch (err) {
            this.error(err)
        }
    }
}