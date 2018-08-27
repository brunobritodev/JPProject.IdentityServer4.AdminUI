import { ExternalProvider } from "./external-provider.model";

export class LoginInfo {
	public allowRememberLogin: boolean;
	public enableLocalLogin: boolean;
	public externalProviders: ExternalProvider[];
	public visibleExternalProviders: ExternalProvider[];
	public isExternalLoginOnly: boolean;
	public externalLoginScheme: string;

	public showProvider(provider: string): boolean {
		if (this.visibleExternalProviders == null)
			return false;

		return this.visibleExternalProviders.find(a => a.displayName.toLowerCase() === provider.toLowerCase()) != null;
	}
}