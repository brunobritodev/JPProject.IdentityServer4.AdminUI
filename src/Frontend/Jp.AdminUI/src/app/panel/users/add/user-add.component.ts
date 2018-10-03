import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { flatMap } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "../../../shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { UserProfile } from "../../../shared/viewModel/userProfile.model";
import { UserService } from "../user.service";


@Component({
    selector: "app-user-add",
    templateUrl: "./user-add.component.html",
    styleUrls: ["./user-add.component.scss"],
    providers: [UserService],
    encapsulation: ViewEncapsulation.None
})
export class UserAddComponent implements OnInit {

    public errors: Array<string>;
    public model: UserProfile;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public showButtonLoading: boolean;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.model = new UserProfile();
        this.errors = [];
        this.showButtonLoading = false;
    }

    public save() {

        this.showButtonLoading = true;
        try {

            this.userService.save(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/users/edit", this.model.userName]);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to register");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to register");
        }

    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }
}
