using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Ung.AcmtSys.Business.Exception;

namespace Ung.AcmtSys.Business
{
    public class BankSavingAccount : BankAccount
    {
        protected readonly Guid AccountId;
        //protected decimal CurrentBalance;
        //private string _accountNumber;
        //protected Account Account;

        public BankSavingAccount(AcmtSysDbEntities context, string accountNumber)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new BankSystemException($"Account number {accountNumber} is not found in system.");
            }

           // Account = account;
            //_accountNumber = account.AccountNumber;
            AccountId = account.AccountId;
        }

        /// <summary>
        /// Deposit the input amount of money into this bank account.
        /// and Charge 0.1%
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public override void DepositMoney(AcmtSysDbEntities context, decimal amount)
        {
            DepositMoney(context, amount, TransactionType.PC, 0.1m);
        }

        public void DepositMoneyFromTransfer(AcmtSysDbEntities context, decimal amount)
        {
            //If deposit from transfer amount then 0 fee amount;
            DepositMoney(context, amount, TransactionType.TRD, 0m);
        }

        private void DepositMoney(AcmtSysDbEntities context, decimal amount, TransactionType transactionType, decimal chargedRate)
        {
            var currentAccount = GetCurrentAccountBalance(context);
            var currentBalance = currentAccount.CurrentBalance;
            //deposit amount transaction
            var masterBankTransactionTypeId = context.MasterBankTransactionTypes.Where(x => x.TransactionType == transactionType.ToString()).Select(y => y.MasterBankTransactionTypeId).First();
            var transactionDeposit = PrepareTransaction(amount, masterBankTransactionTypeId, currentBalance);
            currentBalance += amount;
            transactionDeposit.PostExecutionBalance = currentBalance;
            currentAccount.CurrentBalance = currentBalance;
            context.Transactions.Add(transactionDeposit);

            if (chargedRate > 0)
            {
                //charge amount transaction
                var chargedAmount = amount * chargedRate / 100;
                var chargeTransactionId = context.MasterBankTransactionTypes
                    .Where(x => x.TransactionType == TransactionType.CM.ToString())
                    .Select(y => y.MasterBankTransactionTypeId).First();
                var transactionCharge = PrepareTransaction(chargedAmount, chargeTransactionId, currentBalance);
                currentBalance -= chargedAmount;
                transactionCharge.PostExecutionBalance = currentBalance;
                currentAccount.CurrentBalance = currentBalance;
                context.Transactions.Add(transactionCharge);
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                ReloadAccount(context, currentAccount);
                DepositMoney(context, amount, transactionType, chargedRate);
            }
        }



        /// <inheritdoc />
        /// <summary>
        /// Withdraw the input amount of money from this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public override void WithdrawMoney(AcmtSysDbEntities context, decimal amount)
        {
            WithdrawMoney(context, amount, TransactionType.CS);
        }

        private void WithdrawMoney(AcmtSysDbEntities context, decimal amount, TransactionType transactionType)
        {
            var currentAccount = GetCurrentAccountBalance(context);
            var currentBalance = currentAccount.CurrentBalance;
            if (currentBalance < amount)
            {
                throw new BankSystemException("Insufficient funds");
            }

            var masterBankTransactionTypeId = context.MasterBankTransactionTypes.Where(x => x.TransactionType == transactionType.ToString()).Select(y => y.MasterBankTransactionTypeId).First();
            var transaction = PrepareTransaction(amount, masterBankTransactionTypeId, currentBalance);
            currentBalance -= amount;
            transaction.PostExecutionBalance = currentBalance;
            context.Transactions.Add(transaction);
            currentAccount.CurrentBalance = currentBalance;
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                ReloadAccount(context, currentAccount);
                WithdrawMoney(context, amount, transactionType);
            }
            
        }




        /// <summary>
        /// Transfer the input amount from this account to the given other account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationAccountNumber"></param>
        /// <param name="amount"></param>

        public void TransferMoney(AcmtSysDbEntities context, string destinationAccountNumber, decimal amount)
        {
            //withdraw amount current account
            WithdrawMoney(context, amount, TransactionType.TRW);

            //deposit amount to destination account
            var destinationBankAccount = new BankSavingAccount(context, destinationAccountNumber);
            destinationBankAccount.DepositMoneyFromTransfer(context, amount);
        }





        private Transaction PrepareTransaction(decimal amount, Guid masterBankTransactionTypeId, decimal preCurrentBalance)
        {
            return new Transaction
            {
                TransactionId = Guid.NewGuid(),
                AccountId = AccountId,
                MasterBankTransactionTypeId = masterBankTransactionTypeId,
                TransactionDate = DateTime.UtcNow,
                ValueDate = DateTime.UtcNow,
                CurrencyCode = Currencies.USD.ToString(),
                TransactionAmount = amount,
                TransactionStatus = TransactionStatusType.Complete.ToString(),
                PreExecutionBalance = preCurrentBalance
            };
        }

        private Account GetCurrentAccountBalance(AcmtSysDbEntities context)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccountId == AccountId);
            return account;
        }

        private void ReloadAccount(AcmtSysDbEntities context, Account account)
        {
            context.Entry(account).Reload();
        }
    }
}
