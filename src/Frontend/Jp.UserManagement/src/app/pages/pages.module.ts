import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { UserService } from "../shared/services/user.service";
import { CommonModule } from "@angular/common";
import { AlertModule } from "ngx-bootstrap/alert";
import { UnauthorizedComponent } from "./unauthorized/unauthorized.component";
import { LoginCallbackComponent } from "./login-callback/login-callback.component";
import { RecoverComponent } from "./recover/recover.component";
import { ResetPasswordComponent } from "./reset-password/reset-password.component";
import { ConfirmEmailComponent } from "./confirm-email/confirm-email.component";
import { P404Component } from "./error/404.component";
import { P500Component } from "./error/500.component";
import { NgxMaskModule } from "ngx-mask";
import { TranslateModule } from "@ngx-translate/core";

const routes: Routes = [
    { path: "login", component: LoginComponent, data: { title: "Login Page" } },
    { path: "register", component: RegisterComponent, data: { title: "Register" } },
    { path: "login-callback", component: LoginCallbackComponent, data: { title: "Login" } },
    { path: "unauthorized", component: UnauthorizedComponent, data: { title: "Unauthorized" } },
    { path: "recover", component: RecoverComponent, data: { title: "Recover account" } },
    { path: "reset-password", component: ResetPasswordComponent, data: { title: "Reset password" } },
    { path: "confirm-email", component: ConfirmEmailComponent, data: { title: "Confirm account" } },
    { path: "404", component: P404Component, data: { title: "Not Found" } },
    { path: "500", component: P500Component, data: { title: "Error" } },
];


@NgModule({
    imports: [
        RouterModule.forRoot(routes),
        FormsModule,
        CommonModule,
        TranslateModule,
        ReactiveFormsModule,
        AlertModule.forRoot(),
        NgxMaskModule.forRoot(),
    ],
    providers: [
        UserService,
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        UnauthorizedComponent, 
        LoginCallbackComponent,
        RecoverComponent,
        ResetPasswordComponent,
        ConfirmEmailComponent,
        P404Component,
        P500Component
    ],
    exports: [
        RouterModule
    ]
})
export class PagesModule { }
