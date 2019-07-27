import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from '@env/environment';

export const authConfig: AuthConfig = {
    issuer: environment.IssuerUri,
    clientId: 'IS4-Admin',
    requireHttps: environment.RequireHttps,
    redirectUri: environment.Uri + "/login-callback",
    silentRefreshRedirectUri: environment.Uri + '/silent-refresh.html',
    scope: "openid profile email jp_api.is4",
    silentRefreshTimeout: 5000, // For faster testing
    timeoutFactor: 0.25, // For faster testing
    sessionChecksEnabled: true,
    showDebugInformation: true, // Also requires enabling "Verbose" level in devtools
    clearHashAfterLogin: false, // https://github.com/manfredsteyer/angular-oauth2-oidc/issues/457#issuecomment-431807040
    waitForTokenInMsec: 5000
};
