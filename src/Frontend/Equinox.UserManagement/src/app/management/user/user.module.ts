import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';
import { AlertModule } from 'ngx-bootstrap/alert';
import { AccountComponent } from './account/account.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ProfileService } from './profile/profile.service';

const routes: Routes = [
    {
        path: "",
        data: {
            title: 'User'
        },
        children: [
            { path: 'profile', component: ProfileComponent, data: { title: 'Profiles' } },
            { path: 'account', component: AccountComponent, data: { title: 'Account' } }
        ]
    }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(routes),
        AlertModule.forRoot(),
        ImageCropperModule
    ],
    declarations: [
        ProfileComponent,
        AccountComponent,
    ],
    providers: [
        ProfileService
    ],
    exports: [RouterModule]
})
export class UserModule { }
