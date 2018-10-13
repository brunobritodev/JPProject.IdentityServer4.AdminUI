import { AuthConfig } from "angular-oauth2-oidc";
import { environment } from "@env/environment";
export const authConfig: AuthConfig = {
    issuer: environment.IssuerUri,
    requireHttps: environment.RequireHttps,
    clientId: "IS4-Admin",
    postLogoutRedirectUri: environment.Uri,
    redirectUri: environment.Uri + "/login-callback",
    scope: "openid profile email jp_api.is4",
    oidc: true,
};
