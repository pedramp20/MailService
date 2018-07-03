import { Component } from '@angular/core';
import { MailService } from "../../services/mail.service";
import { Mail } from "../../models/mail";
import { Router, NavigationExtras } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    private mail: Mail;
    private toEmails: string;
    private ccEmails: string;
    private bccEmails: string;
    constructor(private mailService: MailService, private router: Router) {
        this.mail = new Mail();
        this.mail.from = "pedram@mailservice.com";
        this.toEmails = "";
        this.ccEmails = "";
        this.bccEmails = "";
    }
    send() {
        this.mailService.send(this.mail).subscribe(
            response => {
            let navigationExtras: NavigationExtras = {
                queryParams: {
                    "response": JSON.stringify(response)
                }
            }
            this.router.navigate(['/response'], navigationExtras);
        },
            error => {
                //show snackbar
            });
    }
    onToChange(formData: any) {
        this.mail.to = [];
        this.splitAndValidateEmails(this.toEmails, this.mail.to, formData, 'toEmails');
    }

    onCcChange(formData: any) {
        this.mail.cc = [];
        this.splitAndValidateEmails(this.ccEmails, this.mail.cc, formData, 'ccEmails');
    }

    onBccChange(formData: any) {
        this.mail.bcc = [];
        this.splitAndValidateEmails(this.bccEmails, this.mail.bcc, formData, 'bccEmails');
    }

    splitAndValidateEmails(input: string, list: string[], formData: any, controlName: string) {
        let emails = input.split(',');
        for (let email of emails) {
            if (email === "")
                return;
            if (!this.validateEmail(email)) {
                formData.form.controls[controlName].setErrors({ 'invalidFormat': true });
            } else {
                formData.form.controls[controlName].setErrors(null);
                list.push(email);
            }
        }
    }
    validateEmail(email: string) {
        const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }
}
