import { Component } from "@angular/core"
import { MenuItem } from "primeng/api"
import { AppService } from "./app.service"
import { AbstractComponent } from "./components"
import { UserInfo } from "./models"

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent extends AbstractComponent {

  items: MenuItem[] = [
    {
      label: "User",
      routerLink: "/users",
    }
  ]
  user = new UserInfo()

  constructor(service: AppService) {
    super(service)
  }

  isSignedIn() {
    return this.userInfo.id
  }

  async signIn() {
    try {
      this.setProcessing(true)
      let rs = await this.service.post.userForSignIn(this.user)
      this.user = rs
      this.setUserInfo(rs)
      this.setProcessing(false)
    } catch (err) {
      this.error(err)
    }
  }

  signOut() {
    try {
      this.setProcessing(true)
      setTimeout(() => {
        this.setUserInfo(new UserInfo())
        this.setProcessing(false)
      }, 500)
    } catch (err) {
      this.error(err)
    }
  }
}
