import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../shared/shared.module";
import { DndModule } from "ng2-dnd";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { AlertModule } from "ngx-bootstrap/alert";
import { TextMaskModule } from "angular2-text-mask";
import { LoginComponent } from "./login/login.component";
import { Error404Component } from "./error404/error404.component";
import { LoginCallbackComponent } from "./login-callback/login-callback.component";
import { Error500Component } from "./error500/error500.component";

const routes: Routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
    { path: "login", component: LoginComponent },
    { path: "login-callback", component: LoginCallbackComponent },
    { path: "not-found", component: Error404Component },
    { path: "500", component: Error500Component, data: { title: "Error" } },
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
        AlertModule.forRoot(),
        TextMaskModule,
    ],
    declarations: [
        LoginComponent,
        Error404Component,
        LoginCallbackComponent,
        Error500Component
    ],
    exports: [
        RouterModule
    ]
})
export class PagesModule { }
