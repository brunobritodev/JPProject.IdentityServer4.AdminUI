

export class ClientSecret {
    description: string;
    value: string;
    expiration: Date;
    type: string;
}



export class Subject {
    authenticationType: string;
    isAuthenticated: boolean;
    bootstrapContext: any;
    claims: any[];
    label: string;
    name: string;
    nameClaimType: string;
    roleClaimType: string;
}

export class Claim {
    issuer: string;
    originalIssuer: string;
    properties: KeyValuePair;
    subject: Subject;
    type: string;
    value: string;
    valueType: string;
}

export class KeyValuePair {
    key: string;
    value: string;
}

export class Client {
    enabled: boolean;
    clientId: string;
    protocolType: string;
    clientSecrets: ClientSecret[];
    requireClientSecret: boolean;
    clientName: string;
    clientUri: string;
    logoUri: string;
    requireConsent: boolean;
    allowRememberConsent: boolean;
    allowedGrantTypes: string[];
    requirePkce: boolean;
    allowPlainTextPkce: boolean;
    allowAccessTokensViaBrowser: boolean;
    redirectUris: string[];
    postLogoutRedirectUris: string[];
    frontChannelLogoutUri: string;
    frontChannelLogoutSessionRequired: boolean;
    backChannelLogoutUri: string;
    backChannelLogoutSessionRequired: boolean;
    allowOfflineAccess: boolean;
    allowedScopes: string[];
    alwaysIncludeUserClaimsInIdToken: boolean;
    identityTokenLifetime: number;
    accessTokenLifetime: number;
    authorizationCodeLifetime: number;
    absoluteRefreshTokenLifetime: number;
    slidingRefreshTokenLifetime: number;
    consentLifetime: number;
    refreshTokenUsage: number;
    updateAccessTokenClaimsOnRefresh: boolean;
    refreshTokenExpiration: number;
    accessTokenType: number;
    enableLocalLogin: boolean;
    identityProviderRestrictions: string[];
    includeJwtId: boolean;
    claims: Claim[];
    alwaysSendClientClaims: boolean;
    clientClaimsPrefix: string;
    pairWiseSubjectSalt: string;
    allowedCorsOrigins: string[];
    properties: KeyValuePair;
}


