import { Component, OnInit } from "@angular/core";
import { User } from "../../shared/models/user.model";
import { UserService } from "../../shared/services/user.service";
import { Router, ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { debounceTime } from "rxjs/operators";
import { switchMap } from "rxjs/operators";
import { Subject } from "rxjs";
import { DefaultResponse } from "../../shared/view-model/default-response.model";
import { AlertConfig } from "ngx-bootstrap/alert";
import { AccountService } from "../../shared/services/account.services";
import { LoginInfo } from "../../shared/models/login-info.model";

function getAlertConfig(): AlertConfig {
  return Object.assign(new AlertConfig(), { type: "success" });
}

@Component({
  selector: "app-dashboard",
  templateUrl: "register.component.html",
  providers: [
    UserService, 
    { provide: AlertConfig, useFactory: getAlertConfig },
    AccountService ]
})
export class RegisterComponent implements OnInit {

  model: User;
  public errors: Array<string>;
  showButtonLoading: boolean;
  userExist: boolean;
  emailExist: boolean;

  private userExistsSubject: Subject<string> = new Subject<string>();
  private emailExistsSubject: Subject<string> = new Subject<string>();
  loginInfo: LoginInfo;

  constructor(
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private accountService: AccountService) {
  }

  public ngOnInit() {
    this.errors = [];
    this.model = new User();
    this.getLoginInfo();

    this.userExistsSubject
      .pipe(debounceTime(500))
      .pipe(switchMap(a => this.userService.checkUserName(a)))
      .subscribe((response: DefaultResponse<boolean>) => {
        this.userExist = response.data;
      });

    this.emailExistsSubject
      .pipe(debounceTime(500))
      .pipe(switchMap(a => this.userService.checkEmail(a)))
      .subscribe((response: DefaultResponse<boolean>) => {
        this.emailExist = response.data;
      });
  }

  public async getLoginInfo() {
    this.loginInfo = Object.setPrototypeOf((await this.accountService.getLoginInfo().toPromise()).data, LoginInfo.prototype);
  }

  public getClassUsernameExist(): string {
    if (this.model.username == null || this.model.username === "")
      return "";

    return this.userExist ? "is-invalid" : "is-valid";
  }

  public getClassEmailExist(): string {
    if (this.model.email == null || this.model.email === "")
      return "";

    return !this.model.isValidEmail() || this.emailExist ? "is-invalid" : "is-valid";
  }

  public checkIfEmailExists() {
    if (this.model.email == null || this.model.email === "")
      return;

    if (!this.model.isValidEmail())
      return;

    this.emailExistsSubject.next(this.model.email);
  }

  public checkIfUniquenameExists() {
    if (this.model.username == null || this.model.username === "")
      return;
    this.userExistsSubject.next(this.model.username);
  }

  public register() {

    try {
      this.userService.register(this.model).subscribe(
        registerResult => { if (registerResult.data) this.router.navigate(["/login"]); },
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
}
