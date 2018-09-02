import { AuthConfig } from "angular-oauth2-oidc";
export const authConfig: AuthConfig = {

    issuer: "http://localhost:5000",
    clientId: "UserManagementUI",
    postLogoutRedirectUri: "http://localhost:4200/",
    redirectUri: window.location.origin + "/login-callback",
    scope: "openid profile email picture UserManagementApi.owner-content",
    oidc: true,
};