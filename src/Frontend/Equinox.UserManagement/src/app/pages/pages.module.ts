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

const routes: Routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
    { path: "login", component: LoginComponent, data: { title: "Login Page" } },
    { path: "register", component: RegisterComponent, data: { title: "Register" } },
    { path: "login-callback", component: LoginCallbackComponent, data: { title: "Login" } },
    { path: "unauthorized", component: UnauthorizedComponent, data: { title: "Unauthorized" } },
    { path: "recover", component: RecoverComponent, data: { title: "Recover account" } },
    { path: "reset-password", component: ResetPasswordComponent, data: { title: "Reset password" } },
    { path: "confirm-email", component: ConfirmEmailComponent, data: { title: "Confirm account" } },
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
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        UnauthorizedComponent, 
        LoginCallbackComponent,
        RecoverComponent,
        ResetPasswordComponent,
        ConfirmEmailComponent
    ],
    exports: [
        RouterModule
    ]
})
export class PagesModule { }
