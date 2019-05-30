import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from '@env/environment';

export const authProdConfig: AuthConfig = {
    issuer: environment.AuthorityUri,
    clientId: "UserManagementUI",
    requireHttps: environment.RequireHttps,
    redirectUri: environment.Uri + "/login-callback",
    silentRefreshRedirectUri: environment.Uri + '/silent-refresh.html',
    scope: "openid profile email jp_api.user",
    sessionChecksEnabled: true,
    clearHashAfterLogin: false, // https://github.com/manfredsteyer/angular-oauth2-oidc/issues/457#issuecomment-431807040
};
