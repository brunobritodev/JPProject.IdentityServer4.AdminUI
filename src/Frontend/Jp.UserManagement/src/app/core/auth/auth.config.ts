import { AuthConfig } from "angular-oauth2-oidc";
import { environment } from "../../../environments/environment";
export const authConfig: AuthConfig = {

    issuer: environment.IssuerUri,
    requireHttps: environment.RequireHttps,
    clientId: "UserManagementUI",
    postLogoutRedirectUri: "http://localhost:4200/",
    redirectUri: window.location.origin + "/login-callback",
    scope: "openid profile email picture UserManagementApi.owner-content",
    oidc: true,
};