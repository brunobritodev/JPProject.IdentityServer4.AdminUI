import { Component, Input } from "@angular/core";
import { navItems } from "../../_nav";
import { OAuthService } from "../../../../node_modules/angular-oauth2-oidc";

@Component({
  selector: "app-dashboard",
  templateUrl: "./default-layout.component.html"
})
export class DefaultLayoutComponent {
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;
  constructor(private oauthService: OAuthService) {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains("sidebar-minimized");
    });

    this.changes.observe(<Element>this.element, {
      attributes: true
    });
  }

  public logout(){
    this.oauthService.logOut();
  }
}
