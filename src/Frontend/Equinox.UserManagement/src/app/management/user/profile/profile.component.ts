import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { SettingsService } from '../../../core/settings/settings.service';
import { ProfilePictureViewModel } from '../../../shared/view-model/file-upload.model';
import { ProfileService } from './profile.service';
import { DefaultResponse } from '../../../shared/view-model/default-response.model';

@Component({
    templateUrl: 'profile.component.html',
    providers: [ProfileService],

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

    constructor(private settings: SettingsService, private profileService: ProfileService) {

    }

    ngOnInit() {

        this.errors = [];
        this.profileService.getUserData().subscribe((a: DefaultResponse<User>) => {
            this.user = a.data;
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

    public async uploadImage(){
        this.updatingImage = true;
        await this.profileService.updatePicture(this.fileData).toPromise();
        this.updatingImage = false;
    }

    public async updateProfile() {
        this.updatingProfile = true;
        await this.profileService.update(this.user).toPromise();

        this.updatingProfile = false;
    }
}
