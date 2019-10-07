using System;
using System.Linq;
using Ung.AcmtSys.Business.Exception;

namespace Ung.AcmtSys.Business
{
    public class BankSavingAccount : IBankAccount
    {
        protected readonly Guid AccountId;
        protected decimal CurrentBalance;
        protected Account Account;

        public BankSavingAccount(AcmtSysDbEntities context, string accountNumber)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new BankSystemException($"Account number {accountNumber} is not found in system.");
            }

            Account = account;
            CurrentBalance = account.CurrentBalance;
            AccountId = account.AccountId;
        }

        /// <inheritdoc />
        /// <summary>
        /// Deposit the input amount of money into this bank account.
        /// and Charge 0.1%
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public void DepositMoney(AcmtSysDbEntities context, decimal amount)
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
            //var actualDepositAmount = amount - chargedAmount;

            //deposit amount transaction
            var masterBankTransactionTypeId = context.MasterBankTransactionTypes.Where(x => x.TransactionType == transactionType.ToString()).Select(y => y.MasterBankTransactionTypeId).First();
            var transactionDeposit = PrepareTransaction(amount, masterBankTransactionTypeId);
            CurrentBalance += amount;
            transactionDeposit.PostExecutionBalance = CurrentBalance;
            Account.CurrentBalance = CurrentBalance;
            context.Transactions.Add(transactionDeposit);

            if (chargedRate > 0)
            {
                //charge amount transaction
                var chargedAmount = amount * chargedRate / 100;
                var chargeTransactionId = context.MasterBankTransactionTypes
                    .Where(x => x.TransactionType == TransactionType.CM.ToString())
                    .Select(y => y.MasterBankTransactionTypeId).First();
                var transactionCharge = PrepareTransaction(chargedAmount, chargeTransactionId);
                CurrentBalance -= chargedAmount;
                transactionCharge.PostExecutionBalance = CurrentBalance;
                Account.CurrentBalance = CurrentBalance;
                context.Transactions.Add(transactionCharge);
            }


            context.SaveChanges();
        }

        /// <summary>
        /// Withdraw the input amount of money from this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public void WithdrawMoney(AcmtSysDbEntities context, decimal amount)
        {
            WithdrawMoney(context, amount, TransactionType.CS);
        }

        private void WithdrawMoney(AcmtSysDbEntities context, decimal amount, TransactionType transactionType)
        {
            if (CurrentBalance < amount)
            {
                throw new BankSystemException("Insufficient funds");
            }

            var masterBankTransactionTypeId = context.MasterBankTransactionTypes.Where(x => x.TransactionType == transactionType.ToString()).Select(y => y.MasterBankTransactionTypeId).First();
            var transaction = PrepareTransaction(amount, masterBankTransactionTypeId);
            CurrentBalance -= amount;
            transaction.PostExecutionBalance = CurrentBalance;
            context.Transactions.Add(transaction);
            Account.CurrentBalance = CurrentBalance;
            context.SaveChanges();
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
            var destinationBankAccount = BankAccountFactory.CreateInstanceAccount(context, destinationAccountNumber);
            destinationBankAccount.DepositMoneyFromTransfer(context, amount);
        }


        private Transaction PrepareTransaction(decimal amount, Guid masterBankTransactionTypeId)
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
                PreExecutionBalance = CurrentBalance
            };
        }
    }
}
