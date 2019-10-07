import { Component , ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { LoadingDialogService, MessageDialogService } from '../../components';

declare const _ : any;
declare const window : any;

@Component({
    selector: 'deposit-page',
    templateUrl: './deposit-page.component.html',
    styleUrls: ['./deposit-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[attr.page-container]": "true", 
    }
})

export class DepositPageComponent {
    

    public depositForm = {
        accountNo : "",
        accountName : "",
        current : 0,
        deposit : 0
    };







    private clearForm() : void {

        this.depositForm.accountNo = "";
        this.depositForm.accountName = "";
        this.depositForm.current = 0;

        this.depositForm.deposit = 0;

    };

    private async deposit() : Promise<any> {
        await this.httpClient.post(
            [window.config.host,'api/bankaccount/deposit'].join(''),
            {
                account_number : this.depositForm.accountNo,
                deposit_amount : this.depositForm.deposit
            }
        ).toPromise();
    };



    constructor(
        private router : Router,
        private loadingDialogService : LoadingDialogService,
        private messageDialogService : MessageDialogService,
        private httpClient : HttpClient
    ){
        if(!_.isUndefined(window.appState.deposit)){
            this.depositForm.accountName = window.appState.deposit.name;
            this.depositForm.accountNo = window.appState.deposit.no;
            this.depositForm.current = 0;
        }
        else {
            this.router.navigate(['/']);
        }
    }

    public isSetAccount() : boolean {
        if(this.depositForm.accountNo.length > 0){
            return true;
        }
        return false;
    };

    public enabledDeposit() : boolean {

        let enabled = false;

        if(this.depositForm.accountNo.length > 0
            && this.depositForm.deposit > 0){
            enabled = true;
        }

        return enabled;

    };

    public btnCancel() : void {
        this.router.navigate(['/']);
    };

    public btnDeposit() : void {
        if(this.enabledDeposit()){
            const ref = this.loadingDialogService.openDialog({
                text : "Processing..."
            })
            const promise = this.deposit();
            promise.then(()=>{
                this.clearForm();
                ref.dialogRef.close();
                const refM = this.messageDialogService.openAlertDialog("Deposit successfully",null,"Close");
                const promiseM = refM.afterClosed().toPromise();
                promiseM.then(()=>{
                    this.router.navigate(['/']);
                });     
            }).catch(error => {
                ref.dialogRef.close();
                if(error.status == 400){
                    this.messageDialogService.openAlertDialog(error.error,null,"Close");
                }
                else {
                    this.messageDialogService.openAlertDialog("An error occured, please contact your system administrator.",null,"Close");
                } 
            });            
        }
    };

}