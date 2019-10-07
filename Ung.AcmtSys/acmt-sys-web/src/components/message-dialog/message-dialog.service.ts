import { Injectable } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { MessageDialogComponent } from './message-dialog.component';

@Injectable()
export class MessageDialogService {

    constructor(
        private dialog: MatDialog
    ) {

    }

    public openConfirmDialog(title : string, description : string, confirmText : string, cancelText : string) : MatDialogRef<MessageDialogComponent> {
        let dialogRef = this.dialog.open(MessageDialogComponent,{
            width : '400px',
            disableClose : true,
            data : {
                title : title,
                description : description,
                confirmText : confirmText,
                cancelText : cancelText,
                isAlert : false
            }
        });
        return dialogRef;
    };

    public openAlertDialog(title : string, description : string, confirmText : string) : MatDialogRef<MessageDialogComponent> {
        let dialogRef = this.dialog.open(MessageDialogComponent,{
            width : '400px',
            disableClose : true,
            data : {
                title : title,
                description : description,
                confirmText : confirmText,
                cancelText : null,
                isAlert : true
            }
        });
        return dialogRef;
    };




}