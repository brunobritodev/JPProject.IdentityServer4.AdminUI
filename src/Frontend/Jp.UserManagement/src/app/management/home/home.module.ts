import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { TranslateModule } from '@ngx-translate/core';

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
        TranslateModule,
        RouterModule.forChild(routes),
    ],
    declarations: [
        HomeComponent
    ],
    exports: [RouterModule]
})
export class HomeModule {
    
}
