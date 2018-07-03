import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Mail } from "../models/mail";

@Injectable()
export class MailService {
    private mailUrl = 'api/mail';
    constructor(private http: Http) {

    }

    send(mail: Mail) {
        let body = JSON.stringify(mail);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(this.mailUrl, body, options)
            .map(res => res.json());
    }
}
