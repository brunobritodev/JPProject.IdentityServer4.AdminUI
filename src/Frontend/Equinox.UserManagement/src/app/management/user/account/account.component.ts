import { Component, OnInit } from '@angular/core';
import { ChangePassword } from '../../../shared/view-model/change-password.model';
import { SettingsService } from '../../../core/settings/settings.service';
import { AccountManagementService } from '../account-management.service';
import { DefaultResponse } from '../../../shared/view-model/default-response.model';
import { User } from '../../../shared/models/user.model';
import { forkJoin, Observable } from 'rxjs';
import { SetPassword } from '../../../shared/view-model/set-password.model';
import { OAuthService } from 'angular-oauth2-oidc';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    templateUrl: './account.component.html',
    providers: [AccountManagementService]
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
        private oauthService: OAuthService) {

    }

    ngOnInit() {

        forkJoin(
            this.accountManagementService.getUserData(),
            this.accountManagementService.hasPassword()
        )
            .subscribe(([userData, hasPassword]) => {
                this.user = userData.data;
                this.hasPassword = hasPassword.data;
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
                },
                (err) => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.changingPassword = false;
                }
            );

        } catch (error) {
            this.errors.push("Unknown error while trying to register");
            return Observable.throw("Unknown error while trying to register");

        } finally {
            this.changingPassword = false;
        }
    }

    public removeAccount() {
        this.accountManagementService.deleteAccount().subscribe(
            s => {
                this.changingPassword = false;
                this.errors = [];
                this.dangerModal.hide();
                this.oauthService.logOut();

            },
            err => {
                this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                this.changingPassword = false;
            }
        );
    }

    public createPassword() {
        this.changingPassword = true;
        this.accountManagementService.addPassword(this.setPassword).subscribe(
            s => {
                this.changingPassword = false;
                this.errors = [];
                this.hasPassword = true;
            },
            err => {
                this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                this.changingPassword = false;
            }
        );
    }
}
