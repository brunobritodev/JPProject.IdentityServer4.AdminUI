export class Scope {
    constructor() {
        this.userClaims = [];
    }
    /// <summary>
    /// Name of the scope. This is the value a client will use to request the scope.
    /// </summary>
    public name: string;

    /// <summary>
    /// Display name. This value will be used e.g. on the consent screen.
    /// </summary>
    public displayName: string;

    /// <summary>
    /// Description. This value will be used e.g. on the consent screen.
    /// </summary>
    public description: string;

    /// <summary>
    /// Specifies whether the user can de-select the scope on the consent screen. Defaults to false.
    /// </summary>
    public required: boolean;

    /// <summary>
    /// Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes. Defaults to false.
    /// </summary>
    public emphasize: boolean;

    /// <summary>
    /// Specifies whether this scope is shown in the discovery document. Defaults to true.
    /// </summary>
    public showInDiscoveryDocument: boolean;

    public resourceName: string;
    public userClaims: string[];

}
