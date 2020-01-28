import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ClientService } from '@app/clients/clients.service';
import { SettingsService } from '@core/settings/settings.service';
import { TranslatorService } from '@core/translator/translator.service';
import { NewClient } from '@shared/viewModel/client.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { FileViewModel } from '@shared/viewModel/file.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { Observable } from 'rxjs';


@Component({
    selector: "app-client-add",
    templateUrl: "./add.component.html",
    styleUrls: ["./add.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ClientAddComponent implements OnInit {

    public errors: Array<string>;

    public model: NewClient;

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public client: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };

    // Image settings
    public imageChangedEvent: any = '';
    public fileData: ImageCroppedEvent;
    public croppedImage: any = '';
    public showCropper = false;
    public file: any;

    constructor(
        private router: Router,
        public translator: TranslatorService,
        private clientService: ClientService,
        public toasterService: ToasterService,
        public settings: SettingsService) { }

    public ngOnInit() {
        this.errors = [];
        this.model = new NewClient();
        this.showButtonLoading = false;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public selectClient(type: number) {
        this.model.clientType = type;
    }


    public save() {
        this.showButtonLoading = true;
        if(this.fileData != null){
            this.model.logo = new FileViewModel(this.file.name, this.file.type, this.fileData.base64.split(',')[1]);
        }
        this.clientService.save(this.model).subscribe(
            registerResult => {
                if (registerResult) {
                    this.showSuccessMessage();
                    this.router.navigate(['/clients', this.model.clientId, 'edit']);
                }
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

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
        this.fileData = event;
        this.croppedImage = event.base64;
    }
    imageLoaded() {
        this.showCropper = true;
    }
    loadImageFailed() {
        // show message
    }

}
