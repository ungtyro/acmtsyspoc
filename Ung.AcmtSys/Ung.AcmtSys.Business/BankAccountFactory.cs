using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ung.AcmtSys.Business
{
    public static class BankAccountFactory
    {
        public static IBankAccount CreateInstanceAccount(AcmtSysDbEntities context, string accountNumber)
        {
            IBankAccount bankAccount = null;

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

            return bankAccount;
        }
    }
}
