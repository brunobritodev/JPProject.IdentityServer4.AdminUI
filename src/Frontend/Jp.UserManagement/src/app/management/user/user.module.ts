import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';
import { AlertModule } from 'ngx-bootstrap/alert';
import { AccountComponent } from './account/account.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { AccountManagementService } from './account-management.service';
import { NgxMaskModule } from 'ngx-mask';
import { ModalModule } from 'ngx-bootstrap/modal';
import { UserHistoryComponent } from './history/user-history.component';
import { TranslateModule } from '@ngx-translate/core';

const routes: Routes = [
    {
        path: "",
        data: {
            title: 'User'
        },
        children: [
            { path: 'profile', component: ProfileComponent, data: { title: 'Profiles' } },
            { path: 'account', component: AccountComponent, data: { title: 'Account' } },
            { path: 'history', component: UserHistoryComponent, data: { title: 'Account' } }
        ]
    }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(routes),
        AlertModule.forRoot(),
        ImageCropperModule,
        NgxMaskModule.forRoot(),
        ModalModule.forRoot(),
        TranslateModule,
    ],
    declarations: [
        ProfileComponent,
        AccountComponent,
        UserHistoryComponent
    ],
    providers: [
        AccountManagementService
    ],
    exports: [RouterModule]
})
export class UserModule { }
