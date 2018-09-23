import { NgModule } from "@angular/core";
import { ClientListComponent } from "./list/clients-list.component";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { DndModule } from "ng2-dnd";
import { ClientEditComponent } from "./edit/client-edit.component";
import { SpinnersComponent } from "../../shared/components/spinners/spinners.component";
import { ClientSettingsComponent } from "./edit/basic/settings.component";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: ClientListComponent },
    { path: "edit/:clientId", component: ClientEditComponent },
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
        ClientListComponent,
        ClientEditComponent,
        SpinnersComponent,
        ClientSettingsComponent
    ],
    exports: [
        RouterModule
    ]
})
export class ClientsModule { }
