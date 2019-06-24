import { Scope } from "./scope.model";

export class ApiResource {
    constructor() {
        this.enabled = true;
        this.userClaims = [];
    }
    apiSecrets: ApiResourceSecret[];
    scopes: Scope[];
    enabled: boolean;
    name: string;
    displayName: string;
    description: string;
    userClaims: string[];
    oldApiResourceId: string;
}


export class ApiResourceSecret {
    constructor() {
        this.hashType = 0;
    }
    resourceName: string;
    description: string;
    value: string;
    expiration: Date;
    type: string;
    hashType: number;
}
