import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, Observable, throwError as observableThrowError } from 'rxjs';

import { SettingsService } from '../../../core/settings/settings.service';
import { User } from '../../../shared/models/user.model';
import { ChangePassword } from '../../../shared/view-model/change-password.model';
import { DefaultResponse } from '../../../shared/view-model/default-response.model';
import { SetPassword } from '../../../shared/view-model/set-password.model';
import { AccountManagementService } from '../account-management.service';


@Component({
    templateUrl: './account.component.html',
    providers: [AccountManagementService, TranslatorService]
})
export class AccountComponent implements OnInit {

    public changePass: ChangePassword;
    public errors: Array<string>;
    public setPassword: SetPassword;
    public user: User;
    public hasPassword: boolean;

    public dangerModal;
    public changingPassword = false;
    constructor(
        private settings: SettingsService,
        private accountManagementService: AccountManagementService,
        private oauthService: OAuthService,
        private toastr: ToastrService,
        public translator: TranslatorService) {

    }

    ngOnInit() {

        forkJoin(
            this.accountManagementService.getUserData(),
            this.accountManagementService.hasPassword()
        )
            .subscribe(([userData, hasPassword]) => {
                this.user = userData;
                this.hasPassword = hasPassword;
            });
        this.setPassword = new SetPassword();
        this.changePass = new ChangePassword();
        this.errors = [];
    }

    public changePassword() {
        this.changingPassword = true;

        try {
            this.accountManagementService.updatePassword(this.changePass).subscribe(
                (s) => {
                    this.changingPassword = false;
                    this.hasPassword = true;
                    this.toastr.success('Password changed!', 'Success!');
                },
                (err) => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.changingPassword = false;
                }
            );

        } catch (error) {
            this.errors.push("Unknown error while trying to change password");
            return observableThrowError("Unknown error while trying to change password");

        } finally {
            this.changingPassword = false;
        }
    }

    public removeAccount() {
        this.accountManagementService.deleteAccount().subscribe(
            () => {
                this.changingPassword = false;
                this.errors = [];
                this.dangerModal.hide();
                this.oauthService.logOut();
                this.toastr.success('Bye!', 'Success!');
            },
            err => {
                this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                this.changingPassword = false;
            }
        );
    }

    public createPassword() {
        this.changingPassword = true;

        try {
            this.changingPassword = true;
            this.accountManagementService.addPassword(this.setPassword).subscribe(
                s => {
                    this.changingPassword = false;
                    this.errors = [];
                    this.hasPassword = true;
                    this.toastr.success('Password created!', 'Success!');
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.changingPassword = false;
                }
            );

        } catch (error) {
            this.errors.push("Unknown error while trying to create password");
            return observableThrowError("Unknown error while trying to create password");

        } finally {
            this.changingPassword = false;
        }
    }
}
