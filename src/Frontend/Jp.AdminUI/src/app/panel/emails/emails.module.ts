import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { SharedModule } from '@shared/shared.module';
import { ToasterModule } from 'angular2-toaster';
import { TagInputModule } from 'ngx-chips';

import { EmailComponent } from './edit/email.component';
import { EmailService } from './emails.service';


const routes: Routes = [
    { path: "", component: EmailComponent },
];

@NgModule({
    imports: [
        SharedModule,
        ToasterModule.forRoot(),
        RouterModule.forChild(routes),
        TagInputModule,
        AngularEditorModule
    ],
    declarations: [
        EmailComponent
    ],
    providers: [
        EmailService
    ],
    exports: [
        RouterModule
    ]
})
export class EmailModule { }
