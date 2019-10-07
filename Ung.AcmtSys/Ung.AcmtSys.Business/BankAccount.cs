using System;
using System.Linq;

namespace Ung.AcmtSys.Business
{
    public interface IBankAccount
    {
        /// <summary>
        /// Deposit the input amount of money into this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        void DepositMoney(AcmtSysDbEntities context, decimal amount);

        /// <summary>
        /// Deposit the input amount of money into this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        void DepositMoneyFromTransfer(AcmtSysDbEntities context, decimal amount);

        /// <summary>
        /// Withdraw the given amount of money from this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        void WithdrawMoney(AcmtSysDbEntities context, decimal amount);


        /// <summary>
        /// Transfer the input amount from this account to the given other account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationAccountNumber"></param>
        /// <param name="amount"></param>
        void TransferMoney(AcmtSysDbEntities context, string destinationAccountNumber, decimal amount);

    }
}
