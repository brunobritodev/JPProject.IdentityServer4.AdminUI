import { Component, OnInit } from "@angular/core";
import { UserService } from "../../shared/services/user.service";
import { AlertConfig } from "ngx-bootstrap/alert";
import { ForgotPassword } from "../../shared/view-model/forgot-password.model";
import { TranslatorService } from '@core/translator/translator.service';



export function getAlertConfig(): AlertConfig {
    return Object.assign(new AlertConfig(), { type: "success" });
}

@Component({
    selector: "app-dashboard",
    templateUrl: "recover.component.html",
    providers: [
        UserService,
        TranslatorService,
        { provide: AlertConfig, useFactory: getAlertConfig }
    ]
})
export class RecoverComponent implements OnInit {

    public emailSent: boolean;
    public showButtonLoading: boolean;
    public model: ForgotPassword;
    

    constructor(private authService: UserService,
                public translator: TranslatorService) {
    }

    public recover() {
        this.showButtonLoading = true;
        this.authService.recoverPassword(this.model).subscribe(
            recoverResult => {
                this.emailSent = true;
                this.showButtonLoading = false;
            },
            response => {
                this.emailSent = true;
                this.showButtonLoading = false;
            }
        );
    }

    public ngOnInit() {
        this.model = new ForgotPassword();
        this.emailSent = false;
    }

}
