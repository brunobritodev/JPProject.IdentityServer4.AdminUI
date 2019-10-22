export class ProblemDetails {
    
    public success: boolean;

    public static GetErrors(err: any): Array<KeyValuePair> {
        try {
            if (err.status === 403) {
                return [new KeyValuePair("403", "Unauthorized Access")];
            }

            if (err.error.errors)
            {
                if(err.error.errors["DomainNotification"]){
                    return err.error.errors["DomainNotification"].map((element, i) => new KeyValuePair(i, element));
                }
                return err.error.errors.map((element, i) => new KeyValuePair(i, element.message));
            }
                


            return [new KeyValuePair(err.error.status.toString(), "Unknown error - " + err.error.type)];
        } catch (error) {
            return [new KeyValuePair("500", "Unknown error")];
        }
    }
}

export class KeyValuePair {
    constructor(public key: string,
        public value: string) { }
}
