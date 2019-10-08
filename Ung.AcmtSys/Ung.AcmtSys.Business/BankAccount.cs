using System;

namespace Ung.AcmtSys.Business
{
    public class BankAccount : ITransaction
    {
        /// <inheritdoc />
        /// <summary>
        /// Deposit the input amount of money into this bank account. (Not Implement)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public virtual void DepositMoney(AcmtSysDbEntities context, decimal amount)
        {
          
        }

        /// <inheritdoc />
        /// <summary>
        /// Withdraw the given amount of money from this bank account. (Not Implement)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public virtual void WithdrawMoney(AcmtSysDbEntities context, decimal amount)
        {
           
        }
    }
}
