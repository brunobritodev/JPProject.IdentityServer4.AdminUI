import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@shared/shared.module';
import { DndModule } from 'ng2-dnd';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { IdentityResourceAddComponent } from './add/identity-resource-add.component';
import { IdentityResourceEditComponent } from './edit/identity-resource-edit.component';
import { IdentityResourceListComponent } from './list/identity-resources-list.component';

const routes: Routes = [
    { path: "", component: IdentityResourceListComponent },
    { path: ":name/edit", component: IdentityResourceEditComponent },
    { path: "add", component: IdentityResourceAddComponent },
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
        IdentityResourceListComponent,
        IdentityResourceEditComponent,
        IdentityResourceAddComponent
    ],
    exports: [
        RouterModule
    ]
})
export class IdentityResourceModule { }
