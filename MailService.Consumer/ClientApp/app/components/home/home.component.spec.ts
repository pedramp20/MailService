import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { MailService } from "../../services/mail.service";
import { Router, NavigationExtras } from '@angular/router';
import { Observable } from 'rxjs/observable';
import 'rxjs/add/observable/from';
import 'rxjs/add/observable/empty';

describe('HomeComponent', () => {
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;
    let mailService: MailService;
    let router: Router;
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [HomeComponent]
            })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        mailService = TestBed.get(MailService);
        router = TestBed.get(Router);
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should navigate to mail response component, if mail is sent successfully', () => {
        let mailResponse = {
            success: 'true',
            message: 'The mail has been sent successfully.'
        };
        let navigationExtras: NavigationExtras = {
            queryParams: {
                "response": JSON.stringify(mailResponse)
            }
        }
        spyOn(mailService, "send").and.returnValue(Observable.from([mailResponse]));
        let spy = spyOn(router, "navigate");

        component.send();

        expect(spy).toHaveBeenCalledWith(['/response'], navigationExtras);
    });
});
