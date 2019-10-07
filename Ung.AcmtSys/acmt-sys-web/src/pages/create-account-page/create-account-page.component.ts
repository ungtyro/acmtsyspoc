import { Component , ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingDialogService, MessageDialogService } from '../../components';
import { HttpClient, HttpHeaders } from '@angular/common/http';

declare const _ : any;
declare const window : any;

@Component({
    selector: 'create-account-page',
    templateUrl: './create-account-page.component.html',
    styleUrls: ['./create-account-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[attr.page-container]": "true", 
    }
})

export class CreateAccountPageComponent {
    
    public accountOptions = [
        {
            id : "saving",
            title : "Saving"
        }
    ];

    public personalId  = null;

    public accountForm = {
        name : "",
        type : this.accountOptions[0].id
    };





    private async createAccount() : Promise<any> {
        await this.httpClient.post(
            [window.config.host,'api/customers/accounts'].join(''),
            {
                personal_card_id : this.personalId,
                account_name : this.accountForm.name
            }
        ).toPromise();
    };
    
    private clearForm() : void {
        this.accountForm.name = "";
        this.accountForm.type = this.accountOptions[0].id;
    };

    private clearAccountState() : void {
        delete window.appState.accounts;
    };





    constructor(
        private router : Router,
        private loadingDialogService : LoadingDialogService,
        private messageDialogService : MessageDialogService,
        private httpClient : HttpClient
    ){
        if(!_.isUndefined(window.appState.createAccount)){
            this.personalId = window.appState.createAccount.personalId;
        }
        else {
            this.router.navigate(['/']);
        }
    }

    public btnCancel() : void {
        this.router.navigate(['/']);
    };

    public enabledCreate() : boolean {

        let enabled = false;

        if(this.accountForm.name.length > 0
            && this.personalId != null){
            enabled = true;
        }

        return enabled;

    };

    public btnCreate() : void {
        if(this.enabledCreate()){
            const ref = this.loadingDialogService.openDialog({
                text : "Creating..."
            })
            const promise = this.createAccount();
            promise.then(()=>{
                this.clearForm();
                ref.dialogRef.close();
                const refM = this.messageDialogService.openAlertDialog("Create account successfully",null,"Close");
                const promiseM = refM.afterClosed().toPromise();
                promiseM.then(()=>{
                    this.clearAccountState();
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