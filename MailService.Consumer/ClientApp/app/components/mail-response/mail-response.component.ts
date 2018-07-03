import { Component, OnInit } from '@angular/core';
import { MailResponse } from "../../models/mail-response";
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
@Component({
    selector: 'app-mail-response',
    templateUrl: './mail-response.component.html',
    styleUrls: ['./mail-response.component.css']
})
export class MailResponseComponent implements OnInit {
    private response: MailResponse | undefined;
    constructor(private route: ActivatedRoute) {
        this.route.queryParams.subscribe(params => {
            this.response = JSON.parse(params["response"]);
        });
    }

    ngOnInit() {
    }

}
