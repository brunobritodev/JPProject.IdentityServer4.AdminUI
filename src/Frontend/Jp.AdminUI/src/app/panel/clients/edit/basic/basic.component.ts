import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SettingsService } from '@core/settings/settings.service';
import { TranslatorService } from '@core/translator/translator.service';
import { Client } from '@shared/viewModel/client.model';
import { FileViewModel } from '@shared/viewModel/file.model';
import { ImageCroppedEvent } from 'ngx-image-cropper';


@Component({
    selector: "app-client-basic",
    templateUrl: "./basic.component.html",
    styleUrls: ["./basic.component.scss"],
})
export class ClientBasicComponent implements OnInit {

    
    // Image settings
    public imageChangedEvent: any = '';
    public fileData: ImageCroppedEvent;
    public croppedImage: any = '';
    public showCropper = false;
    public file: any;

    @Input()
    public model: Client;
    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService,
        public settings: SettingsService) { }


    ngOnInit() {

    }

    fileChangeEvent(event: any): void {
        if (event == null || event.target == null)
            return;

        const fileToUpload = event.target.files.item(0);
        this.file = fileToUpload;
        const reader = new FileReader();

        reader.readAsDataURL(fileToUpload);

        this.imageChangedEvent = event;
        this.showCropper = true;
    }
    imageCropped(event: ImageCroppedEvent) {
        this.model.logo = new FileViewModel(this.file.name, this.file.type, event.base64.split(',')[1]);
        this.croppedImage = event.base64;
    }
    imageLoaded() {
        this.showCropper = true;
    }
    loadImageFailed() {
        // show message
    }
}
