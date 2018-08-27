import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { UserService } from "../shared/services/user.service";
import { CommonModule } from "@angular/common";
import { AlertModule } from "ngx-bootstrap/alert";
import { AccountService } from "../shared/services/account.service";
import { UnauthorizedComponent } from "./unauthorized/unauthorized.component";
import { LoginCallbackComponent } from "./login-callback/login-callback.component";

const routes: Routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
    { path: "login", component: LoginComponent, data: { title: "Login Page" } },
    { path: "register", component: RegisterComponent, data: { title: "Register Page" } },
    { path: "login-callback", component: LoginCallbackComponent, data: { title: "Login Page" } },
    { path: "unauthorized", component: UnauthorizedComponent, data: { title: "Unauthorized Page" } },
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
        RegisterComponent,
        UnauthorizedComponent, 
        LoginCallbackComponent 
    ],
    exports: [
        RouterModule
    ]
})
export class PagesModule { }
