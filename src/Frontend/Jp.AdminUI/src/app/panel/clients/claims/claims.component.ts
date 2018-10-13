import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { ClientService } from "@app/clients/clients.service";
import { flatMap, tap, map } from "rxjs/operators";
import { ClientClaim } from "@shared/viewModel/client.model";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { ScopeService } from "@shared/services/scope.service";
import { StandardClaims } from "@shared/viewModel/standard-claims.model";


@Component({
    selector: "app-client-claim",
    templateUrl: "./claims.component.html",
    styleUrls: ["./claims.component.scss"],
    providers: [ClientService, ScopeService],
    encapsulation: ViewEncapsulation.None
})
export class ClientClaimsComponent implements OnInit {

    public errors: Array<string>;
    public claimsSuggested: Array<string>;
    public model: ClientClaim;
    public claims: ClientClaim[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public client: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public standardClaims: string[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private clientService: ClientService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.client = p["clientId"])).pipe(map(p => p["clientId"])).pipe(flatMap(m => this.clientService.getClientClaims(m.toString()))).subscribe(result => this.claims = result.data);
        this.errors = [];
        this.model = new ClientClaim();
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public selectType(type: string) {
        this.model.type = type;
    }


    public remove(id: number) {

        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.clientService.removeClaim(this.client, id).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadClaims();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to register");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to register");
        }

    }

    private loadClaims(): void {
        this.clientService.getClientClaims(this.client).subscribe(c => this.claims = c.data);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.clientId = this.client;
            this.clientService.saveClaim(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadClaims();
                        this.model = new ClientClaim();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to register");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to register");
        }
    }

}
