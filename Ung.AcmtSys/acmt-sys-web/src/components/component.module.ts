import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { 
    MatDialogModule, MatProgressSpinnerModule, MatButtonModule
} from '@angular/material';

import { LoadingDialogComponent, LoadingDialogService } from './loading-dialog';
import { MessageDialogComponent, MessageDialogService } from './message-dialog';

@NgModule({
  declarations: [
    LoadingDialogComponent,
    MessageDialogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatButtonModule
  ],
  providers: [
    LoadingDialogService,
    MessageDialogService
  ],
  exports : [
    LoadingDialogComponent,
    MessageDialogComponent
  ],
  entryComponents : [
    LoadingDialogComponent,
    MessageDialogComponent
  ]
})
export class ComponentModule { }