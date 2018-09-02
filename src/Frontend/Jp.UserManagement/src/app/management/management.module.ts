
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../core/auth/auth.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../core/interceptors/AuthInterceptor';
import { ModalModule } from 'ngx-bootstrap/modal';

const routes: Routes = [
    {
        path: "", canActivate: [AuthGuard], data: { title: 'Management' },
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