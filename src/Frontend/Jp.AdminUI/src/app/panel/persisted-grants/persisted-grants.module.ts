import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@shared/shared.module';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { PersistedGrantListComponent } from './list/persisted-grants-list.component';

const routes: Routes = [
    { path: "", component: PersistedGrantListComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        NgxSelectModule,
        TagInputModule,
        NgbPaginationModule
    ],
    declarations: [
        PersistedGrantListComponent,
    ],
    exports: [
        RouterModule
    ]
})
export class PersistedGrantsModule { }
