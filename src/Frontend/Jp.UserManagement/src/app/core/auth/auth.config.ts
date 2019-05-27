import { AuthConfig } from "angular-oauth2-oidc";
import { environment } from "@env/environment";
export const authConfig: AuthConfig = {
    issuer: environment.AuthorityUri,
    requireHttps: environment.RequireHttps,
    clientId: "UserManagementUI",
    postLogoutRedirectUri: environment.Uri,
    redirectUri: environment.Uri + "/login-callback",
    silentRefreshRedirectUri: environment.Uri + '/silent-refresh.html',
    scope: "openid profile email jp_api.user",
    oidc: true,
};
