import { Component , ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoadingDialogService, MessageDialogService } from '../../components';

declare const _ : any;
declare const window : any;

@Component({
    selector: 'customer-bank-accounts-page',
    templateUrl: './customer-bank-accounts-page.component.html',
    styleUrls: ['./customer-bank-accounts-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[attr.page-container]": "true", 
    }
})

export class CustomerBankAccountsPageComponent {
    
    public customerSearchOptions = [
        {
            id : "PSID",
            title : "Personal Id"
        },
        {
            id : "PPID",
            title : "Passport Id"
        }
    ];

    public customerSearchForm = {
        type : this.customerSearchOptions[0].id,
        text : ""
    };

    public bankAccountSearchOptions = [
        {
            id : "ANO",
            title : "Account No."
        },
        {
            id : "ANM",
            title : "Account Name"
        }
    ];

    public bankAccountSearchForm = {
        type : this.bankAccountSearchOptions[0].id,
        text : ""
    };

    public customerInfo = {
        id : null,
        firstname : "",
        lastname : "",
        personalId : "",
        birthDate : "",
        gender : "",
        adress :""
    };
    
    public genderType = {
        M : 'Male',
        F : 'Femal'
    };

    public accounts = [];

    private searchCustomer() : void {
        let refLoading = this.loadingDialogService.openDialog({ text : "Searching..." });
        let promise = this.httpClient.get(
            [window.config.host,'api/customers/',this.customerSearchForm.text].join('')
        ).toPromise();
        promise.then(result => {
            refLoading.dialogRef.close();
            if(result != null){
                this.customerSearchForm.text = "";
                this.customerInfo.id = result['customer_id'];
                this.customerInfo.firstname = result['first_name'];
                this.customerInfo.lastname = result['last_name'];
                this.customerInfo.birthDate = result['birth_date'];
                this.customerInfo.personalId = result['personal_card_id'];
                this.customerInfo.gender = result['sex'];
                this.customerInfo.adress = result['address'];
                this.saveCustomerState();
                this.getAllAccounts();
            }
            else {
                let refBox = this.messageDialogService.openAlertDialog(
                    'Search customer',
                    'Not Found ' + this.customerSearchForm.text,
                    "Close"
                );
            }
        }).catch(error => {
            refLoading.dialogRef.close();
            if(error.status == 400){
                this.messageDialogService.openAlertDialog(error.error,null,"Close");
            }
            else {
                this.messageDialogService.openAlertDialog("An error occured, please contact your system administrator.",null,"Close");
            } 
        });
    };

    private async getAccounts(personalId : string,accountNo : string) : Promise<any>{
        return await this.httpClient.get(
            [window.config.host,'api/customers/',personalId,'/accounts/',accountNo].join('')
        ).toPromise();
    };

    private searchAccounts(accountNo : string) : void {
        let refLoading = this.loadingDialogService.openDialog({ text : "Searching..." });
        let promise = this.getAccounts(this.customerInfo.personalId,accountNo);
        promise.then(result => {
            refLoading.dialogRef.close();
            if(result == null){
                let refBox = this.messageDialogService.openAlertDialog(
                    'Search account',
                    'Not Found '+this.bankAccountSearchForm.text ,
                    "Close"
                );
            }
            else {
                this.bankAccountSearchForm.text = "";
                let accounts = result;
                if(!_.isArray(result)){
                    accounts = [result];
                }
                this.setAccounts(accounts);
                this.saveAccountState();
            }            
        }).catch(error => {
            refLoading.dialogRef.close();
            if(error.status == 400){
                this.messageDialogService.openAlertDialog(error.error,null,"Close");
            }
            else {
                this.messageDialogService.openAlertDialog("An error occured, please contact your system administrator.",null,"Close");
            } 
        });
    };

    private getAllAccounts() : void {
        let promise = this.getAccounts(this.customerInfo.personalId,null);
        promise.then(result => {
            this.setAccounts(result);
            this.saveAccountState();
        }).catch(error => {

        });
    };

    private setAccounts(accounts : Array<any>) : void {
        this.accounts = [];
        if(accounts != null){
            for(let i in accounts){
                this.accounts.push({
                    name : accounts[i].account_name,
                    no : accounts[i].account_number
                });
            }
        }        
    };

    private saveCustomerState() : void {
        window.appState.customerInfo = this.customerInfo;
    };

    private saveAccountState() : void {
        window.appState.accounts = this.accounts;
    };

    private getCustomerState() : void {
        if(!_.isUndefined(window.appState.customerInfo)){
            this.customerInfo = window.appState.customerInfo;
        }        
    };

    private getAccountState() : void {
        if(!_.isUndefined(window.appState.accounts)){
            this.accounts = window.appState.accounts;
        }        
    };

    private deposit(account : any) : void {
        window.appState.deposit = account;
        this.router.navigate(['/deposit']);
    };

    private transfer(account : any) : void {
        window.appState.transfer = account;
        this.router.navigate(['/transfer']);
    };

    private createAccount() : void {
        window.appState.createAccount = {
            personalId : this.customerInfo.personalId
        };
        this.router.navigate(['/create-account']);
    };



    constructor(
        private router : Router,
        private httpClient : HttpClient,
        private loadingDialogService : LoadingDialogService,
        private messageDialogService : MessageDialogService,
    ){
        this.getCustomerState();
        this.getAccountState();
        if(this.customerInfo.id != null && this.accounts.length == 0){
            this.getAllAccounts();
        }
    }

    public btnDeposit(account : any) : void {        
        this.deposit(account);
    };

    public btnTransfer(account : any) : void {
        this.transfer(account);
    };

    public btnNewCustomer() : void {
        this.router.navigate(['/create-customer']);
    };

    public btnNewAccount() : void {
        this.createAccount();
    };

    public btnCustomerSearch() : void {
        this.searchCustomer();
    };

    onKeydown(event) {
        if (event.key === "Enter") {
            this.searchCustomer();
        }
    }

    public btnAccountSearch() : void {
        this.searchAccounts(this.bankAccountSearchForm.text);
    };

    onKeydownAccount(event) {
        if (event.key === "Enter") {
            this.searchAccounts(this.bankAccountSearchForm.text);
        }
    }

}