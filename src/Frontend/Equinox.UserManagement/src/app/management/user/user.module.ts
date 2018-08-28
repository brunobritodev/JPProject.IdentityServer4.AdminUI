// Angular
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
    {
        path: "",
        data: {
            title: 'User'
        },
        children: [
            {
                path: 'profile',
                component: ProfileComponent,
                data: {
                    title: 'Profiles'
                }
            }
        ]
    }
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(routes),
    ],
    declarations: [
        ProfileComponent
    ],
    exports: [RouterModule]
})
export class UserModule { }
