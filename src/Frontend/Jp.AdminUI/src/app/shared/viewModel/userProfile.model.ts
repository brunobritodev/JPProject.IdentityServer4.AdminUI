export class UserProfile {
    public email: string;
    public name: string;
    public picture: string;
    public userName: string;
    public emailConfirmed: boolean;
    public phoneNumberConfirmed: boolean;
    public twoFactorEnabled: boolean;
    public lockoutEnd?: Date;
    public lockoutEnabled: boolean;
    public accessFailedCount: number;
    public securityStamp: string;
    public bio: string;
    public url: string;
    public company: string;
    public phoneNumber: string;
    public jobTitle: string;
    public password: string;
    public confirmPassword: string;

    public isValidEmail(): boolean {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(this.email).toLowerCase());
    }
}

export class ListOfUsers {
    public total: number;
    public users: Array<UserProfile>;
}
