import { Component, Input, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { GlobalSettings } from '@shared/viewModel/global-settings.model';
import { RecaptchaSettings } from '@shared/viewModel/recaptcha-settings';
import { SMTP } from '@shared/viewModel/smtp.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

import { GlobalSettingsService } from '../global-settings.service';


@Component({
    selector: "app-recaptcha",
    templateUrl: "./recaptcha.component.html",
    styleUrls: ["./recaptcha.component.scss"]
})
export class RecaptchaSettingsComponent implements OnInit {

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true,
        timeout: 60000
    });

    showButtonLoading: boolean;

    public settings: RecaptchaSettings;

    @Input()
    public errors: Array<string>;

    @Input()
    public model: Array<GlobalSettings>;
    useRecaptcha: boolean;

    constructor(
        public translator: TranslatorService,
        public toasterService: ToasterService,
        private settingsServices: GlobalSettingsService) { }

    ngOnInit() {

        this.settings = new RecaptchaSettings();
        this.settings.siteKey = GlobalSettings.getSetting(this.model, "Recaptcha:SiteKey");
        this.settings.privateKey = GlobalSettings.getSetting(this.model, "Recaptcha:PrivateKey");
        this.settings.useRecaptcha = GlobalSettings.getSetting(this.model, "UseRecaptcha");

        this.useRecaptcha = this.settings.useRecaptcha.value == "true";
    }


    public updateSettings() {
        this.errors.splice(0, this.errors.length);
        let configurations = new Array<GlobalSettings>();
        configurations.push(this.settings.siteKey);
        configurations.push(this.settings.privateKey);
        configurations.push(this.settings.useRecaptcha);
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

    public changeUseRecaptcha() {
        this.settings.useRecaptcha.value = this.useRecaptcha.toString();
    }
}
