import { Component, OnInit } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { SettingsService } from '../../../core/settings/settings.service';
import { FileUpload } from '../../../shared/view-model/file-upload.model';
import { ProfileService } from './profile.service';
import { DefaultResponse } from '../../../shared/view-model/default-response.model';
import { ImageCropperComponent, CropperSettings } from 'ngx-img-cropper';

@Component({
    templateUrl: './profile.component.html',
    providers: [ProfileService]
})
export class ProfileComponent implements OnInit {

    public user: User;
    public errors: Array<string>;
    uploadingImage: boolean;
    public imageChangedEvent: string;
    public croppedImage: string;
    public imageReadyToUpload = false;
    public fileData: FileUpload;

    cropper: ImageCropperComponent;
    cropperSettings: CropperSettings;

    constructor(private settings: SettingsService, private profileService: ProfileService) {

    }

    ngOnInit() {
        this.errors = [];
        this.profileService.getUserData().subscribe((a: DefaultResponse<User>) => {
            this.user = a.data;
        });

        this.cropperSettings = new CropperSettings();
        this.cropperSettings.width = 100;
        this.cropperSettings.height = 100;
        this.cropperSettings.croppedWidth = 100;
        this.cropperSettings.croppedHeight = 100;
        this.cropperSettings.canvasWidth = 400;
        this.cropperSettings.canvasHeight = 300;


    }

    // fileChangeEvent(event: any): void {
    //     this.uploadingImage = true;

    //     const fileToUpload = event.target.files.item(0);
    //     const reader = new FileReader();

    //     reader.readAsDataURL(fileToUpload);
    //     reader.onload = () => {
    //         this.fileData = new FileUpload(
    //             fileToUpload.name,
    //             fileToUpload.type,
    //             reader.result.toString().split(',')[1]
    //         );
    //     };
    //     this.imageChangedEvent = event;
    // }

    fileChangeEvent(event: any): void {
        this.uploadingImage = true;

        const image: any = new Image();
        const fileToUpload: File = event.target.files[0];
        const reader: FileReader = new FileReader();

        reader.onloadend = (loadEvent: any) => {
            image.src = loadEvent.target.result;
            this.cropper.setImage(image);
            this.fileData = new FileUpload(
                fileToUpload.name,
                fileToUpload.type,
                reader.result.toString().split(',')[1]
            );
        };

        reader.readAsDataURL(fileToUpload);
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
