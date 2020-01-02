
export class KeyValuePair {
  constructor(public key: string,
      public value: string) { }
}

export class ProblemDetails {

    public success: boolean;

    public static GetErrors(err: any): Array<KeyValuePair> {
        try {
            if (err.status === 403 || err.status === 404) {
                return [new KeyValuePair("403", "Unauthorized Access")];
            }

            if (err.error.errors) {
                if (err.error.errors["DomainNotification"]) {
                    return err.error.errors["DomainNotification"].map((element, i) => new KeyValuePair(i, element));
                }
                if (Array.isArray(err.error.errors)) { return err.error.errors.map((element, i) => new KeyValuePair(i, element.message)); }

                let mappedErrors = [];
                Object.keys(err.error.errors).map(function (key, index) {
                    mappedErrors.push(new KeyValuePair(key, err.error.errors[key]));
                });
                return mappedErrors;
            }



            return [new KeyValuePair(err.error.status.toString(), "Unknown error - " + err.error.type)];
        } catch (error) {
            return [new KeyValuePair("500", "Unknown error")];
        }
    }
}

