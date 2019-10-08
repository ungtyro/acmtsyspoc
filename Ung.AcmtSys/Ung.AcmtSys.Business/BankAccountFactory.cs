using System;
using System.Linq;
using Ung.AcmtSys.Business.Exception;

namespace Ung.AcmtSys.Business
{
    public static class BankAccountFactory
    {
        public static BankAccount CreateInstanceAccount(AcmtSysDbEntities context, string accountNumber)
        {
            BankAccount bankAccount = null;

            var accountType = context.Accounts.Where(x => x.AccountNumber == accountNumber)
                .Select(y => y.MasterBankAccountType.AccountType).FirstOrDefault();

            if (accountType != null)
            {
                var bankAccountType = (BankAccountType)Enum.Parse(typeof(BankAccountType), accountType, true);
                switch (bankAccountType)
                {
                    case BankAccountType.ST:
                        bankAccount = new BankSavingAccount(context, accountNumber);
                        break;

                        //Other case implement

                }
            }
            else
            {

                throw new BankSystemException($"Account number {accountNumber} is not found in system.");

            }

            return bankAccount;
        }
    }
}
