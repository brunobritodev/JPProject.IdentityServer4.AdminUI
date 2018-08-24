import { Component, OnInit } from "@angular/core";
import { UserService } from "../../shared/services/user.service";
import { Router, ActivatedRoute } from "@angular/router";
import { AccountService } from "../../shared/services/account.services";
import { LoginInfo } from "../../shared/models/login-info.model";
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from "angular-6-social-login-v2";

@Component({
  selector: "app-dashboard",
  templateUrl: "login.component.html"
})
export class LoginComponent implements OnInit {
  loginInfo: LoginInfo;

  constructor(
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private accountService: AccountService,
    private socialAuthService: AuthService) {
  }

  public ngOnInit() {
    this.getLoginInfo();
  }

  private async getLoginInfo() {
    this.loginInfo = Object.setPrototypeOf((await this.accountService.getLoginInfo().toPromise()).data, LoginInfo.prototype);
    
  }
  
  public socialSignIn(socialPlatform: string) {
    let socialPlatformProvider = "";
    if (socialPlatform === "facebook") {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    } else if (socialPlatform === "google") {
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    }


    this.socialAuthService.signIn(socialPlatformProvider).then(
      (userData) => {
        console.log(socialPlatform + " sign in data : ", userData);
        // Now sign-in with userData
        // ...
        this.accountService.externalLogin(userData).toPromise().then(a => console.log(a));
      }
    );
  }


}
