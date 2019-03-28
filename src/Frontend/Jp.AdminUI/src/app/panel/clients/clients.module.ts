import { NgModule } from "@angular/core";
import { ClientListComponent } from "./list/clients-list.component";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "@shared/shared.module";
import { DndModule } from "ng2-dnd";
import { ClientEditComponent } from "./edit/client-edit.component";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { ClientAuthComponent } from "./edit/auth/auth.component";
import { ClientSettingsComponent } from "./edit/settings/settings.component";
import { ClientTokenComponent } from "./edit/token/token.component";
import { ClientBasicComponent } from "./edit/basic/basic.component";
import { ClientSecretsComponent } from "./secrets/secrets.component";
import { ClientPropertiesComponent } from "./properties/properties.component";
import { ClientClaimsComponent } from "./claims/claims.component";
import { ClientAddComponent } from "./add/add.component";
import { ClientDeviceFlowComponent } from "./edit/device-flow/device-flow.component";
import { ClientService } from "./clients.service";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: ClientListComponent },
    { path: "edit/:clientId", component: ClientEditComponent },
    { path: "secrets/:clientId", component: ClientSecretsComponent },
    { path: "properties/:clientId", component: ClientPropertiesComponent },
    { path: "claims/:clientId", component: ClientClaimsComponent },
    { path: "add", component: ClientAddComponent },
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
        ClientSettingsComponent,
        ClientAuthComponent,
        ClientTokenComponent,
        ClientDeviceFlowComponent,
        ClientBasicComponent,
        ClientSecretsComponent,
        ClientPropertiesComponent,
        ClientClaimsComponent,
        ClientAddComponent
    ],
    providers: [
        ClientService
    ],
    exports: [
        RouterModule
    ]
})
export class ClientsModule { }
