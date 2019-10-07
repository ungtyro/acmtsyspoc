import { Component, ViewEncapsulation, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
    selector: 'loading-dialog',
    templateUrl: 'loading-dialog.component.html',
    styleUrls: ['loading-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class LoadingDialogComponent {

    public data = {
        text : null
    };

    constructor(
        private dialogRef: MatDialogRef<LoadingDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private params: any
    ) {    
        this.data = params;
    }   

}