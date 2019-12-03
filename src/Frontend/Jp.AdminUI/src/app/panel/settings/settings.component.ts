import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { GlobalSettings } from '@shared/viewModel/global-settings.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

import { GlobalSettingsService } from './global-settings.service';


@Component({
    selector: "app-settings",
    templateUrl: "./settings.component.html",
    styleUrls: ["./settings.component.scss"],
})
export class SettingsComponent implements OnInit {

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true,
        timeout: 60000
    });

    showButtonLoading: boolean;

    public errors: Array<string>;

    settingSection = 1;
    public settings: Array<GlobalSettings>;

    constructor(
        public translator: TranslatorService,
        public toasterService: ToasterService,
        private settingsServices: GlobalSettingsService) { }

    ngOnInit() {
        this.errors = [];
        this.settings = [];
        this.settingsServices.getSettings().subscribe(s => this.settings = s);
    }

}
