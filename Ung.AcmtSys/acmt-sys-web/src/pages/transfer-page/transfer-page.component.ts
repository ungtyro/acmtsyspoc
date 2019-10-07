import { Component , ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { LoadingDialogService, MessageDialogService } from '../../components';

declare const _ : any;
declare const window : any;

@Component({
    selector: 'transfer-page',
    templateUrl: './transfer-page.component.html',
    styleUrls: ['./transfer-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[attr.page-container]": "true", 
    }
})

export class TransferPageComponent {
    

    public searchTypes = [
        {
            id : 1,
            title : "ชื่อบัญชี"
        },
        {
            id : 2,
            title : "เลขบัญชี"
        },
        {
            id : 3,
            title : "ID/Passport"
        }
    ];

    public destinationSearchForm = {
        type : this.searchTypes[0].id,
        text : ""
    };

    public transferForm = {
        source : {
            accountNo : "",
            accountName : "",
            current : 0
        },
        destination : {
            accountNo : "",
            accountName : "",
            current : 0
        },
        transfer : 0
    };








    private clearForm() : void {

        this.transferForm.source.accountNo = "";
        this.transferForm.source.accountName = "";
        this.transferForm.source.current = 0;

        this.transferForm.destination.accountNo = "";
        this.transferForm.destination.accountName = "";
        this.transferForm.destination.current = 0;

        this.transferForm.transfer = 0;

    };

    private async transfer() : Promise<any> {
        await this.httpClient.post(
            [window.config.host,'api/bankaccount/direct-transfer'].join(''),
            {
                source_account_number : this.transferForm.source.accountNo,
                destination_account_number : this.transferForm.destination.accountNo,
                transfer_amount : this.transferForm.transfer
            }
        ).toPromise();
    };






    constructor(
        private router : Router,
        private loadingDialogService : LoadingDialogService,
        private messageDialogService : MessageDialogService,
        private httpClient : HttpClient
    ){
        if(!_.isUndefined(window.appState.transfer)){
            this.transferForm.source.accountName = window.appState.transfer.name;
            this.transferForm.source.accountNo = window.appState.transfer.no;
            this.transferForm.source.current = 0;
        }
        else {
            this.router.navigate(['/']);
        }
    }

    public isSetAccount() : boolean {
        if(this.transferForm.source.accountNo.length > 0
            && this.transferForm.destination.accountNo.length > 0){
            return true;
        }
        return false;
    };

    public enabledTransfer() : boolean {

        let enabled = false;

        if(this.transferForm.source.accountNo.length > 0
            && this.transferForm.destination.accountNo.length > 0
            && this.transferForm.transfer > 0){
            enabled = true;
        }

        return enabled;

    };

    public btnDestinationSearch() : void {
        
    };

    public btnTransfer() : void {
        if(this.enabledTransfer()){
            const ref = this.loadingDialogService.openDialog({
                text : "Transfering..."
            })
            const promise = this.transfer();
            promise.then(()=>{
                this.clearForm();
                ref.dialogRef.close();
                const refM = this.messageDialogService.openAlertDialog("Transfer successfully",null,"Close");
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

    public btnCancel() : void {
        this.router.navigate(['/']);
    };

}