import { Claim } from "./claim.model";

export class ClientSecret {
    constructor() {
        this.hashType = 0;
    }
    clientId: string;
    description: string;
    value: string;
    expiration: Date;
    type: string;
    hashType: number;
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


export class ClientClaim extends Claim {
    clientId: string;
}

export class KeyValuePair {
    key: string;
    value: string;
}


export class ClientProperty {
    id: number;
    clientId: string;
    key: string;
    value: string;
}

export class NewClient {
    clientId: string;
    clientName: string;
    clientUri: string;
    logoUri: string;
    description: string;
    clientType: number;
}

export class Client {
    enabled: boolean;
    clientId: string;
    description: string;
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
    properties: ClientProperty[];
    oldClientId: string;
    userCodeType: string;
    deviceCodeLifetime: number;
    
    public static isValid(client: Client, errors: string[]): boolean {
        errors.length = 0;
        if (client.allowedGrantTypes == null || client.allowedGrantTypes.length <= 0) {
            errors.push("Invalid Grant Types");
        }

        // spaces are not allowed in grant types
        client.allowedGrantTypes.forEach(grant => {
            if (grant.indexOf(' ') >= 0) {
                errors.push("Grant types cannot contain spaces");
            }
        });

        // single grant type, seems to be fine
        if (client.allowedGrantTypes.length == 1) return true;

        // would allow response_type downgrade attack from code to token
        Client.findGrantTypes('implicit', 'authorization_code', client.allowedGrantTypes, errors);
        Client.findGrantTypes('implicit', 'hybrid', client.allowedGrantTypes, errors);

        Client.findGrantTypes('authorization_code', 'hybrid', client.allowedGrantTypes, errors);

        return errors.length <= 0;
    }

    public static findGrantTypes(grantA: string, grantB: string, grants: string[], errors: string[]) {
        if (grants.find(g => g == grantA) != null && grants.find(g => g == grantB) != null)
            errors.push(`Grant types list cannot contain both ${grantA} and ${grantB}`);

    }
}


