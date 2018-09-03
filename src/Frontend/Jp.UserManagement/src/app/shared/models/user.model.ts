
export class User {
	public email: string;
	public password: string;
	public confirmPassword: string;
	public phoneNumber: string;
	public name: string;
    public username: string;
    public picture: string;
    public provider: string;
    public providerId: string;
    public jobTitle: string;
    public bio: string;
    public url: string;

	public isValidEmail(): boolean {
		const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		return re.test(String(this.email).toLowerCase());
	}

}