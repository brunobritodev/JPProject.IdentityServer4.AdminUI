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
}
