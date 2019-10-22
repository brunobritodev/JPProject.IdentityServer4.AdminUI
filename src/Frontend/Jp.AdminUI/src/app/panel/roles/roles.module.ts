import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@shared/shared.module';
import { DndModule } from 'ng2-dnd';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { RoleAddComponent } from './add/role-add.component';
import { RoleEditComponent } from './edit/role-edit.component';
import { RolesListComponent } from './list/roles-list.component';

const routes: Routes = [
    { path: "", component: RolesListComponent },
    { path: ":role/edit", component: RoleEditComponent },
    { path: "add", component: RoleAddComponent }
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
        RolesListComponent,
        RoleEditComponent,
        RoleAddComponent
    ],
    exports: [
        RouterModule
    ]
})
export class RoleModule { }
