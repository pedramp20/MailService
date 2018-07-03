export class Mail {
    constructor() {
        this.to = [];
        this.cc = [];
        this.bcc = [];
    }
    from: string | undefined;
    to: string[];
    cc: string[];
    bcc: string[];
    subject: string | undefined;
    body: string | undefined;
}