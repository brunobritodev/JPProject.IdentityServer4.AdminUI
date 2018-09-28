import { Claim } from "./client.model";

export class IdentityResource {
required: boolean;
        emphasize: boolean;
        showInDiscoveryDocument: boolean;
        enabled: boolean;
        name: string;
        displayName: string;
        description: string;
        userClaims: Claim[];
}
