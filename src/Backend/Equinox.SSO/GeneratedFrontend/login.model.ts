import { ExternalProvider } from "./external-provider.model";
import { ExternalProvider } from "./external-provider.model";

export class Login   {
	public allowRememberLogin: boolean;
	public enableLocalLogin: boolean;
	public externalProviders: ExternalProvider[];
	public visibleExternalProviders: ExternalProvider[];
	public isExternalLoginOnly: boolean;
	public externalLoginScheme: string;
}