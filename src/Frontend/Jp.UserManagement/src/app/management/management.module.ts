
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardWithForcedLogin } from '../core/auth/auth-guard-with-forced-login.service';

const routes: Routes = [
    {
        path: "", canActivate: [AuthGuardWithForcedLogin], data: { title: 'Management' },
        children: [
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", loadChildren: "app/management/home/home.module#HomeModule" },
            { path: "user", loadChildren: "app/management/user/user.module#UserModule" },
        ]
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
    ],
    providers: [        
    ],
    exports: [RouterModule]
})
export class ManagementModule{}