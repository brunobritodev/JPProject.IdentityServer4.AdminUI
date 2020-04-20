import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@shared/shared.module';
import { ToasterModule } from 'angular2-toaster';
import { TagInputModule } from 'ngx-chips';

import { PersistedGrantListComponent } from './list/persisted-grants-list.component';

const routes: Routes = [
    { path: "", component: PersistedGrantListComponent },
];

@NgModule({
    imports: [
        SharedModule,
        ToasterModule.forRoot(),
        RouterModule.forChild(routes),
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
