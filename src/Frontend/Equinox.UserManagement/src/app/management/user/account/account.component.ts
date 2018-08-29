import { Component, OnInit } from '@angular/core';
import { ChangePassword } from '../../../shared/view-model/change-password.model';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    templateUrl: './account.component.html',
})
export class AccountComponent implements OnInit {

    public changePass: ChangePassword;
    public errors: Array<string>;

    constructor(private settings: SettingsService) {
       
     }

    ngOnInit() {
         this.changePass = new ChangePassword();
        this.errors = [];
    }

}
