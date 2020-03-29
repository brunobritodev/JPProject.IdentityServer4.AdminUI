import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@shared/shared.module';

import { HomeComponent } from './home.component';

const routes: Routes = [
    { path: "", component: HomeComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
    ],
    declarations: [
        HomeComponent
    ],
    exports: [
        RouterModule
    ]
})
export class HomeModule { }
