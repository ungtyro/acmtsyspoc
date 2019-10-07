import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
    MatButtonModule, MatSelectModule, MatInputModule, MatIconModule,
    MatProgressSpinnerModule, MatRippleModule, MatCardModule, MatFormFieldModule,
    MatDatepickerModule, MatNativeDateModule
} from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { ComponentModule } from '../components';

import { AppComponent } from './app.component';

import {
    MainMasterPageComponent,
    CreateAccountPageComponent,
    DepositPageComponent,
    TransferPageComponent,
    CustomerBankAccountsPageComponent,
    CreateCustomerPageComponent
} from '../pages';

@NgModule({
    declarations: [
        AppComponent,
        MainMasterPageComponent,
        CreateAccountPageComponent,
        DepositPageComponent,
        TransferPageComponent,
        CustomerBankAccountsPageComponent,
        CreateCustomerPageComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ComponentModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatSelectModule,
        MatInputModule,
        MatIconModule,
        MatProgressSpinnerModule,
        MatRippleModule,
        MatCardModule,
        MatFormFieldModule,
        MatDatepickerModule,
        MatNativeDateModule
    ],
    providers: [
        MatDatepickerModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
