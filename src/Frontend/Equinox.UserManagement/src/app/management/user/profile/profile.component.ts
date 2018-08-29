import { Component, OnInit } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { SettingsService } from '../../../core/settings/settings.service';
import { FileUpload } from '../../../shared/view-model/file-upload.model';

@Component({
    templateUrl: './profile.component.html',
})
export class ProfileComponent implements OnInit {

    public user: User;
    public errors: Array<string>;
    uploadingImage: boolean;
    public imageChangedEvent: any = '';
    public croppedImage: any = '';
    public imageReadyToUpload = false;
    public fileData: FileUpload;

    constructor(private settings: SettingsService) {
        this.errors = [];
    }

    ngOnInit() {

        this.settings.getUserProfile().subscribe((a: any) => {
            this.user = new User();
            this.user.name = a.name;
            this.user.email = a.email;
            this.user.picture = a.picture;
        });

    }

    fileChangeEvent(event: any): void {
        this.uploadingImage = true;

        const fileToUpload = event.target.files.item(0);
        const reader = new FileReader();
        
        reader.readAsDataURL(fileToUpload);
        reader.onload = () => {
            this.fileData = new FileUpload(
                fileToUpload.name,
                fileToUpload.type,
                reader.result.toString().split(',')[1]
            );

        };
        this.imageChangedEvent = event;
    }
    imageCropped(image: string) {
        this.fileData.value = image.split(',')[1];
        this.croppedImage = image;
    }
    imageLoaded() {
        this.imageReadyToUpload = true;
    }
    loadImageFailed() {
        // show message
    }

}
