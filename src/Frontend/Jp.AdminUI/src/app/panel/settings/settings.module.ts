import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@shared/shared.module';
import { DndModule } from 'ng2-dnd';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { EmailSettingsComponent } from './emails/email-settings.component';
import { GlobalSettingsService } from './global-settings.service';
import { SettingsComponent } from './settings.component';
import { StorageSettingsComponent } from './storage/storage-settings.component';


const routes: Routes = [
    { path: "", component: SettingsComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
        NgxSelectModule,
        TagInputModule
    ],
    declarations: [
        SettingsComponent,
        EmailSettingsComponent,
        StorageSettingsComponent
    ],
    providers: [
        GlobalSettingsService
    ],
    exports: [
        RouterModule
    ]
})
export class SettingsModule { }
