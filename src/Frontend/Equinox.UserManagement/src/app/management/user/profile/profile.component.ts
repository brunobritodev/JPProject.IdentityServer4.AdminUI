import { Component, OnInit } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { ChangePassword } from '../../../shared/models/change-password.model';
import { SettingsService } from '../../../core/settings/settings.service';

@Component({
    templateUrl: './profile.component.html',
})
export class ProfileComponent implements OnInit {

    public user: User;
    public changePass: ChangePassword;
    public errors: Array<string>;

    constructor(private settings: SettingsService) {
        this.changePass = new ChangePassword();
        this.errors = [];
     }

    ngOnInit() {
        
        this.settings.getUserProfile().subscribe( (a: any) => {
            this.user = new User();
            this.user.name = a.name;
            this.user.email = a.email;
            this.user.picture = a.picture;
        });
        
    }

}
