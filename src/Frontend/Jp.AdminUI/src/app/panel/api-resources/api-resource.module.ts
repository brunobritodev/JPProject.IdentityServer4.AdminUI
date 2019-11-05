import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@shared/shared.module';
import { DndModule } from 'ng2-dnd';
import { TagInputModule } from 'ngx-chips';
import { NgxSelectModule } from 'ngx-select-ex';

import { ApiResourceAddComponent } from './add/api-resource-add.component';
import { ApiResourceEditComponent } from './edit/api-resource-edit.component';
import { ApiResourceListComponent } from './list/api-resources-list.component';
import { ApiResourceScopesComponent } from './scope/api-scopes.component';
import { ApiResourceSecretsComponent } from './secrets/api-secrets.component';

const routes: Routes = [
    { path: "", component: ApiResourceListComponent },
    { path: ":name/edit", component: ApiResourceEditComponent },
    { path: "add", component: ApiResourceAddComponent },
    { path: ":resource/secrets", component: ApiResourceSecretsComponent },
    { path: ":resource/scopes", component: ApiResourceScopesComponent },
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
        ApiResourceListComponent,
        ApiResourceEditComponent,
        ApiResourceAddComponent,
        ApiResourceSecretsComponent,
        ApiResourceScopesComponent
    ],
    exports: [
        RouterModule
    ]
})
export class ApiResourceModule { }
