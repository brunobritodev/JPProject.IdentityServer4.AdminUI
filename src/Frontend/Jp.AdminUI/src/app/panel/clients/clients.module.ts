import { NgModule } from "@angular/core";
import { ClientListComponent } from "./list/clients-list.component";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { DndModule } from "ng2-dnd";
import { ClientEditComponent } from "./edit/client-edit.component";
import { SpinnersComponent } from "../../shared/components/spinners/spinners.component";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { ClientAuthComponent } from "./edit/auth/auth.component";
import { ClientSettingsComponent } from "./edit/settings/settings.component";
import { NumberDirective } from "../../shared/directives/numberCheck/numbers-only.directive";
import { ClientTokenComponent } from "./edit/token/token.component";
import { ClientBasicComponent } from "./edit/basic/basic.component";
import { ClientSecretsComponent } from "./secrets/secrets.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: ClientListComponent },
    { path: "edit/:clientId", component: ClientEditComponent },
    { path: "secrets/:clientId", component: ClientSecretsComponent },
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
        ClientSettingsComponent,
        ClientAuthComponent,
        NumberDirective,
        ClientTokenComponent,
        ClientBasicComponent,
        ClientSecretsComponent
    ],
    exports: [
        RouterModule
    ]
})
export class ClientsModule { }
