import { Component , ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { LoadingDialogService, MessageDialogService } from '../../components';
import { HttpClient, HttpHeaders } from '@angular/common/http';

declare const window : any;

@Component({
    selector: 'create-customer-page',
    templateUrl: './create-customer-page.component.html',
    styleUrls: ['./create-customer-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[attr.page-container]": "true", 
    }
})

export class CreateCustomerPageComponent {
    
    public customerForm = {
        firstname : "",
        lastname : "",
        personalId : "",
        gender : "M"
    };

    private s4() : string {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    };

    private newGuild() : string{
        return [this.s4(),this.s4(),'-',this.s4(),'-',this.s4(),'-',this.s4(),'-',this.s4(),this.s4(),this.s4()].join("");
    };

    private async createCustomer() : Promise<any> {
        await this.httpClient.post(
            [window.config.host,'api/customers'].join(''),
            {
                customer : {
                    customer_id : this.newGuild(),
                    personal_card_id : this.customerForm.personalId,
                    first_name : this.customerForm.firstname,
                    last_name : this.customerForm.lastname,
                    sex : this.customerForm.gender
                }
            }
        ).toPromise();
    };

    private async hasCustomer() : Promise<boolean> {
        let result = <boolean>await this.httpClient.get(
            [window.config.host,'api/customers/verification/',this.customerForm.personalId].join('')
        ).toPromise();
        return result;
    };

    private clearForm() : void {
        this.customerForm.firstname = "";
        this.customerForm.lastname = "";
        this.customerForm.personalId = "";
        this.customerForm.gender = "M";
    };

    private clearCustomerState() : void {
        delete window.appState.customerInfo;
    };






    constructor(
        private router : Router,
        private loadingDialogService : LoadingDialogService,
        private messageDialogService : MessageDialogService,
        private httpClient : HttpClient
    ){

    }

    public btnCancel() : void {
        this.router.navigate(['/']);
    };

    public enabledCreate() : boolean {
        let enabled = false;

        if(this.customerForm.personalId != null
            && this.customerForm.personalId.length > 0){
            enabled = true;
        }
    
        return enabled;
    };

    public btnCreate() : void {
        if(this.enabledCreate()){
            const ref2 = this.loadingDialogService.openDialog({
                text : "Verify..."
            })
            let promise2 = this.hasCustomer();
            promise2.then(has => {
                ref2.dialogRef.close();
                if(!has){
                    const ref = this.loadingDialogService.openDialog({
                        text : "Creating..."
                    })
                    const promise = this.createCustomer();
                    promise.then(()=>{
                        this.clearForm();
                        ref.dialogRef.close();
                        const refM = this.messageDialogService.openAlertDialog("Create customer successfully",null,"Close");
                        const promiseM = refM.afterClosed().toPromise();
                        promiseM.then(()=>{
                            this.clearCustomerState();
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
                else {
                    this.messageDialogService.openAlertDialog("This Customer has already exist in the system.",null,"Close");
                }
            }).catch(error => {
                ref2.dialogRef.close();
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