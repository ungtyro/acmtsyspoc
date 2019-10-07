import { Component , ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'main-master-page',
    templateUrl: './main-master-page.component.html',
    styleUrls: ['./main-master-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[attr.page-container]": "true", 
    }
})

export class MainMasterPageComponent {
    
    public selectedMenuId = 1;

    constructor(
        private router : Router
    ){

    }

    public btnCreateAccount() : void {
        this.selectedMenuId = 1;
        this.router.navigate(['/create-account']);
    };

    public btnDeposit() : void {
        this.selectedMenuId = 2;
        this.router.navigate(['/deposit']);
    };

    public btnTransfer() : void {
        this.selectedMenuId = 3;
        this.router.navigate(['/transfer']);
    };

}