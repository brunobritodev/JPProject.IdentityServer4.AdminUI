import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { TextMaskModule } from 'angular2-text-mask';
import { AlertModule } from 'ngx-bootstrap/alert';

import { SharedModule } from '../shared/shared.module';
import { Error404Component } from './error404/error404.component';
import { Error500Component } from './error500/error500.component';
import { LoginCallbackComponent } from './login-callback/login-callback.component';
import { LoginComponent } from './login/login.component';

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
