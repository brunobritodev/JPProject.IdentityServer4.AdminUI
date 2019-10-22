import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { OAuthService } from 'angular-oauth2-oidc';
import * as jsonpatch from 'fast-json-patch';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs/operators';

import { SettingsService } from '../../../core/settings/settings.service';
import { User } from '../../../shared/models/user.model';
import { ProfilePictureViewModel } from '../../../shared/view-model/file-upload.model';
import { AccountManagementService } from '../account-management.service';

@Component({
    templateUrl: 'profile.component.html',
    providers: [AccountManagementService,TranslatorService],

})
export class ProfileComponent implements OnInit {

    public user: User;
    public errors: Array<string>;
    uploadingImage: boolean;
    public imageChangedEvent: any = '';
    public croppedImage: any = '';
    public imageReadyToUpload = false;
    public fileData: ProfilePictureViewModel;
    updatingProfile: boolean;
    updatingImage: boolean;
    userProfile: object;
    patchObserver: jsonpatch.Observer<User>;

    constructor(
        private settings: SettingsService, 
        private profileService: AccountManagementService,
        private toastr: ToastrService,
        public translator: TranslatorService,
        private authService: OAuthService
    ) {

    }

    ngOnInit() {
        this.authService.loadUserProfile().then(a => this.userProfile = a);
        this.errors = [];
        this.profileService.getUserData()
        .pipe(
            tap(user => this.patchObserver = jsonpatch.observe(user))
        )
        .subscribe((a: User) => {
            this.user = a;
        });
    }

    fileChangeEvent(event: any): void {
        if (event == null)
            return;
        this.uploadingImage = true;

        const fileToUpload = event.target.files.item(0);
        const reader = new FileReader();

        reader.readAsDataURL(fileToUpload);
        reader.onload = () => {
            this.fileData = new ProfilePictureViewModel(
                fileToUpload.name,
                fileToUpload.type,
                reader.result.toString().split(',')[1]
            );
            this.uploadingImage = false;
            this.imageReadyToUpload = true;
            
        };
        this.imageChangedEvent = event;
    }
    imageCropped(image: string) {
        this.fileData.value = image.split(',')[1];
        this.croppedImage = image;
    }
    imageLoaded() {
    }
    loadImageFailed() {
        // show message
    }

    public async uploadImage() {
        this.updatingImage = true;
        await this.profileService.updatePicture(this.fileData).toPromise();
        this.user.picture = this.croppedImage;
        this.toastr.success('Picture updated, refresh browser!', 'Success!');
        this.updatingImage = false;
    }

    public async updateProfile() {
        this.updatingProfile = true;
        await this.profileService.update(this.user).toPromise();
        this.toastr.success('Profile Updated!', 'Success!');
        this.updatingProfile = false;
    }
}
