import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ClientService } from '@app/clients/clients.service';
import { TranslatorService } from '@core/translator/translator.service';
import { NewClient } from '@shared/viewModel/client.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';


@Component({
    selector: "app-client-add",
    templateUrl: "./add.component.html",
    styleUrls: ["./add.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ClientAddComponent implements OnInit {

    public errors: Array<string>;

    public model: NewClient;

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public client: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };

    constructor(
        private router: Router,
        public translator: TranslatorService,
        private clientService: ClientService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.errors = [];
        this.model = new NewClient();
        this.showButtonLoading = false;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public selectClient(type: number) {
        this.model.clientType = type;
    }


    public save() {
        this.showButtonLoading = true;
        this.clientService.save(this.model).subscribe(
            registerResult => {
                if (registerResult) {
                    this.showSuccessMessage();
                    this.router.navigate(['/clients/edit', this.model.clientId]);
                }
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

}
