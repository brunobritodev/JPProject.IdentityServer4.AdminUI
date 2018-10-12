export class DefaultResponse<T> {
    public success: boolean;
    public data: T;

    public static GetErrors(err: any): Array<KeyValuePair> {

        if (err.status === 403) {
            return [new KeyValuePair("403", "Unauthorized Access")];
        }

        if (err.error.errors != null)
            return err.error.errors.map((element, i) => new KeyValuePair(i, element));

        let errors = Object.keys(err.error).map(element => new KeyValuePair(element, err.error[element][0]));
        if (errors[0] == undefined) {
            errors = [];
            return [new KeyValuePair("500", "Unknown error while trying to update")];
        }
    }
}

export class KeyValuePair {
    constructor(public key: string,
        public value: string) { }
}
