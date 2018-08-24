import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { UserService } from "../shared/services/user.service";
import { CommonModule } from "@angular/common";
import { AlertModule } from "ngx-bootstrap/alert";
import { AccountService } from "../shared/services/account.services";

const routes: Routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
    { path: "login", component: LoginComponent, data: { title: "Login Page" } },
    { path: "register", component: RegisterComponent, data: { title: "Register Page" } }
];


@NgModule({
    imports: [
        RouterModule.forRoot(routes),
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        AlertModule.forRoot(),
    ],
    providers: [
        UserService,
        AccountService
    ],
    declarations: [
        LoginComponent,
        RegisterComponent
    ],
    exports: [
        RouterModule
    ]
})
export class PagesModule { }
