import { UserLoginInfo } from "./user-login-info.model";
import { AuthenticationScheme } from "./authentication-scheme.model";

export class ExternalLogins   {
	public currentLogins: UserLoginInfo[];
	public otherLogins: AuthenticationScheme[];
	public showRemoveButton: boolean;
	public statusMessage: string;
}