import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';


const routes: Routes = [
    {
        path: "", component: HomeComponent,
        data: {
            title: 'Welcome'
        },
    }
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(routes),
    ],
    declarations: [
        HomeComponent
    ],
    exports: [RouterModule]
})
export class HomeModule {

}
