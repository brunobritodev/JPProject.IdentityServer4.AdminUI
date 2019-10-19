import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { Claim } from '@shared/viewModel/claim.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { StandardClaims } from '@shared/viewModel/standard-claims.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';
import { debounceTime, flatMap, map, tap } from 'rxjs/operators';


@Component({
    selector: "app-user-claim",
    templateUrl: "./user-claims.component.html",
    styleUrls: ["./user-claims.component.scss"],
    providers: [UserService],
})
export class UserClaimsComponent implements OnInit {

    public errors: Array<string>;
    public claimsSuggested: Array<string>;
    public model: Claim;
    public claims: Claim[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public userName: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public standardClaims: string[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.userName = p["username"])).pipe(map(p => p["username"])).pipe(flatMap(m => this.userService.getUserClaims(m.toString()))).subscribe(result => this.claims = result);
        this.errors = [];
        this.model = new Claim();
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public selectType(type: string) {
        this.model.type = type;
    }


    public remove(type: string) {

        this.showButtonLoading = true;
        this.errors = [];
        this.userService.removeClaim(this.userName, type).subscribe(
            () => {
                this.showSuccessMessage();
                this.loadClaims();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }

    private loadClaims(): void {
        this.userService.getUserClaims(this.userName).subscribe(c => this.claims = c);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        this.userService.saveClaim(this.userName, this.model).subscribe(
            registerResult => {
                if (registerResult) {
                    this.showSuccessMessage();
                    this.loadClaims();
                    this.model = new Claim();
                }
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

}
