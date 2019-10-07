import { Component } from '@angular/core';

declare const window: any;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'ung-acmtsys-web';

    constructor(

    ) {
        window.appState = {};
        window.config = {
            host : "https://acmtsysservice.azurewebsites.net/"
        };
    }
}
