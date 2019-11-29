import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { SharedModule } from '@shared/shared.module';
import { DndModule } from 'ng2-dnd';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { EmailComponent } from './edit/email.component';
import { EmailService } from './emails.service';


const routes: Routes = [
    { path: "", component: EmailComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
        NgxSelectModule,
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
