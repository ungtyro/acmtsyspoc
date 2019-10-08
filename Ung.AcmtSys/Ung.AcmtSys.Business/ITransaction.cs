namespace Ung.AcmtSys.Business
{
    public interface ITransaction
    {
        /// <summary>
        /// Deposit the input amount of money into this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        void DepositMoney(AcmtSysDbEntities context, decimal amount);

        /// <summary>
        /// Withdraw the given amount of money from this bank account.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        void WithdrawMoney(AcmtSysDbEntities context, decimal amount);

    }
}
