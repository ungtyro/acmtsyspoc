import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    MainMasterPageComponent,
    CreateAccountPageComponent,
    DepositPageComponent,
    TransferPageComponent,
    CustomerBankAccountsPageComponent,
    CreateCustomerPageComponent
} from '../pages';

const routes: Routes = [
    {
        path: 'ddd',
        component: MainMasterPageComponent,
        children: [
            {
                path : '',
                redirectTo : 'create-account',
                pathMatch: 'full'
            },
            {
                path: 'create-account',
                component: CreateAccountPageComponent
            },
            {
                path: 'deposit',
                component: DepositPageComponent
            },
            {
                path: 'transfer',
                component: TransferPageComponent
            }
        ]
    },
    {
        path: '',
        component: CustomerBankAccountsPageComponent
    },
    {
        path: 'deposit',
        component: DepositPageComponent
    },
    {
        path: 'transfer',
        component: TransferPageComponent
    },
    {
        path: 'create-account',
        component: CreateAccountPageComponent
    },
    {
        path: 'create-customer',
        component: CreateCustomerPageComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
