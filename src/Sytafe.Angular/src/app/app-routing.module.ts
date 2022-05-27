import { NgModule } from "@angular/core"
import { RouterModule, Routes } from "@angular/router"
import { HomePage, UserPage, UsersPage } from "./pages";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "home" },
  { component: HomePage, path: "home" },
  { component: UserPage, path: "user/:id" },
  { component: UsersPage, path: "users" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
