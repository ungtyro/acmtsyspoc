import { Injectable } from '@angular/core';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { LoadingDialogComponent } from './loading-dialog.component';

@Injectable()
export class LoadingDialogService {

    constructor(
        private dialog: MatDialog
    ) {

    }

    public openDialog(params : {text : string}) : { 
        dialogRef : MatDialogRef<LoadingDialogComponent>,
        setText : Function
    } {

        let dialogRef = this.dialog.open(LoadingDialogComponent,{
            minWidth : '150px',
            minHeight : '100px',
            disableClose : true,
            data : params
        });

        return {
            dialogRef : dialogRef,
            setText : (text : string)=> {
                params.text = text;
            }
        };
    };

}