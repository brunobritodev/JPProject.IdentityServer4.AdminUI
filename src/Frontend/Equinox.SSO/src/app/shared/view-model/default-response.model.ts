export class DefaultResponse<T> {
    public success: boolean;
    public data: T;

    public static GetErrors(err: any): Array<KeyValuePair> {
        if (err.error.errors != null)
            return err.error.errors.map((element, i) => new KeyValuePair(i, element));

        return Object.keys(err.error).map(element => new KeyValuePair(element, err.error[element][0]));

    }
}

export class KeyValuePair {
    constructor(public key: string,
        public value: string) { }
}
