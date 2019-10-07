using System;

namespace Ung.AcmtSys.Business.Exception
{
   public class BankSystemException :ApplicationException
    {
        public BankSystemException(string message): base(message)
        {

        }
    }
}
