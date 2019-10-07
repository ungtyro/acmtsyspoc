using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ung.AcmtSys.Business.Models;

namespace Ung.AcmtSys.Business
{

    public enum Sex
    {
        [ConstantDescription("Male")]
        M,
        [ConstantDescription("Female")]
        F
    }

    public enum CustomerType
    {
        Personal,
        LegalEntity
    }

    public enum CustomerStatusType
    {
        Active,
        InActive,
        Delete
    }

    public enum AccountStatusType
    {
        Open,
        Close
    }

    public enum Currencies
    {
        USD
    }

    public enum BankAccountType
    {
        [ConstantDescription("Saving Account")]
        ST
    }


    public enum TransactionType
    {
        [ConstantDescription("Commission Fee")]
        CM,
        [ConstantDescription("Pay-in cash")]
        PC,
        [ConstantDescription("Transfer from Other Account")]
        TRD,
        [ConstantDescription("Transfer to Other Account")]
        TRW,
        [ConstantDescription("Cash Withdraw")]
        CS
    }

    public enum TransactionStatusType
    {
        Complete
    }
}
