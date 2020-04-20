import { Component, Input, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { GlobalSettings } from '@shared/viewModel/global-settings.model';
import { SMTP } from '@shared/viewModel/smtp.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

import { GlobalSettingsService } from '../global-settings.service';


@Component({
    selector: "app-email-settings",
    templateUrl: "./email-settings.component.html",
    styleUrls: ["./email-settings.component.scss"]
})
export class EmailSettingsComponent implements OnInit {

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true,
        timeout: 60000
    });

    showButtonLoading: boolean;

    public settings: SMTP;

    @Input()
    public errors: Array<string>;

    @Input()
    public model: Array<GlobalSettings>;
    sendMail: boolean;
    useSsl: boolean;

    constructor(
        public translator: TranslatorService,
        public toasterService: ToasterService,
        private settingsServices: GlobalSettingsService) { }

    ngOnInit() {

        this.settings = new SMTP();
        this.settings.username = GlobalSettings.getSetting(this.model, "Smtp:Username");
        this.settings.password = GlobalSettings.getSetting(this.model, "Smtp:Password");
        this.settings.server = GlobalSettings.getSetting(this.model, "Smtp:Server");
        this.settings.port = GlobalSettings.getSetting(this.model, "Smtp:Port");
        this.settings.useSsl = GlobalSettings.getSetting(this.model, "Smtp:UseSsl");
        this.settings.sendMail = GlobalSettings.getSetting(this.model, "SendEmail");

        this.sendMail = this.settings.sendMail.value == "true";
        this.useSsl = this.settings.useSsl.value == "true";
    }

    public updateSettings() {
        this.errors.splice(0, this.errors.length);
        let configurations = new Array<GlobalSettings>();
        configurations.push(this.settings.username);
        configurations.push(this.settings.password);
        configurations.push(this.settings.sendMail);
        configurations.push(this.settings.server);
        configurations.push(this.settings.port);
        configurations.push(this.settings.useSsl);
        this.showButtonLoading = true;


        this.settingsServices.update(configurations).subscribe(
            () => {
                this.showSuccessMessage();
                this.showButtonLoading = false;
            },
            err => {
                ProblemDetails.GetErrors(err).map(a => a.value).forEach(i => this.errors.push(i));
                this.showButtonLoading = false;
            }
        );
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public changeUseSsl() {
        this.settings.useSsl.value = this.useSsl.toString();
    }

    public changeSendEmail() {
        this.settings.sendMail.value = this.sendMail.toString();
    }
}
