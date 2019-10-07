import { Component, ViewEncapsulation, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

declare const _ : any;

@Component({
    selector: 'message-dialog',
    templateUrl: 'message-dialog.component.html',
    styleUrls: ['message-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class MessageDialogComponent {

    public title = "";
    public description = "";
    public confirmText = "Confirm";
    public cancelText = "Cancel";
    public isAlert = false;

    constructor(
        private dialogRef: MatDialogRef<MessageDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any
    ) {    
        this.title = data.title;
        this.description = data.description;
        if(!_.isUndefined(data.confirmText) && data.confirmText != null){
            this.confirmText = data.confirmText;
        }
        if(!_.isUndefined(data.cancelText) && data.cancelText != null){
            this.cancelText = data.cancelText;
        }
        this.isAlert = data.isAlert;
    }   

    public btnConfirm() : void {
        this.dialogRef.close(true);
    };

    public btnCancel() : void {
        this.dialogRef.close(false);
    };


}