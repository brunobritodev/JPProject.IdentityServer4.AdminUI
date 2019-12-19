
export class Bcc {
  recipients: string[];
}

export class Sender {
  address: string;
  name:    string;
}

export class Email {
    public constructor() {
        this.bcc = new Bcc();
        this.sender = new Sender();
        this.content = "";
    }
    type:    number;
    content: string;
    subject: string;
    bcc:     Bcc;
    sender:  Sender;
}
