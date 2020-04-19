import { Component, Input, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { GlobalSettings } from '@shared/viewModel/global-settings.model';
import { SMTP } from '@shared/viewModel/smtp.model';
import { StorageSettings } from '@shared/viewModel/storage-settings.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

import { GlobalSettingsService } from '../global-settings.service';


@Component({
    selector: "app-storage-settings",
    templateUrl: "./storage-settings.component.html",
    styleUrls: ["./storage-settings.component.scss"]
})
export class StorageSettingsComponent implements OnInit {

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true,
        timeout: 60000
    });

    showButtonLoading: boolean;

    public settings: StorageSettings;

    @Input()
    public errors: Array<string>;

    @Input()
    public model: Array<GlobalSettings>;
    useStorage: boolean;

    constructor(
        public translator: TranslatorService,
        public toasterService: ToasterService,
        private settingsServices: GlobalSettingsService) { }

    ngOnInit() {

        this.settings = new StorageSettings();
        this.settings.username = GlobalSettings.getSetting(this.model, "Storage:Username");
        this.settings.password = GlobalSettings.getSetting(this.model, "Storage:Password");
        this.settings.service = GlobalSettings.getSetting(this.model, "Storage:Service");
        this.settings.virtualPath = GlobalSettings.getSetting(this.model, "Storage:VirtualPath");
        this.settings.useStorage = GlobalSettings.getSetting(this.model, "UseStorage");
        this.settings.basePath = GlobalSettings.getSetting(this.model, "Storage:BasePath");
        this.settings.storageName = GlobalSettings.getSetting(this.model, "Storage:StorageName");
        this.settings.physicalPath = GlobalSettings.getSetting(this.model, "Storage:PhysicalPath");

        this.useStorage = this.settings.useStorage.value == "true";
    }

    public updateSettings() {
        this.errors.splice(0, this.errors.length);
        let configurations = new Array<GlobalSettings>();

        configurations.push(this.settings.service);
        configurations.push(this.settings.useStorage);

        if (this.settings.service.value === "S3") {
            configurations.push(this.settings.username);
            configurations.push(this.settings.password);
            configurations.push(this.settings.storageName);
        }
        if (this.settings.service.value === "Azure") {
            configurations.push(this.settings.username);
            configurations.push(this.settings.password);
        }
        if (this.settings.service.value === "Local") {
            configurations.push(this.settings.virtualPath);
            configurations.push(this.settings.basePath);
        }

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

    public changeStorage() {
        this.settings.useStorage.value = this.useStorage.toString();
    }
    public changeTab(tabName: string) {
        this.settings.service.value = tabName;
    }
}
