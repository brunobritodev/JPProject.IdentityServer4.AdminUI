
export class IdentityResource {
    constructor() {
        this.enabled = true;
        this.showInDiscoveryDocument = true;
        this.userClaims = [];
    }
    required: boolean;
    emphasize: boolean;
    showInDiscoveryDocument: boolean;
    enabled: boolean;
    name: string;
    displayName: string;
    description: string;
    userClaims: string[];
    oldName:string;
}
