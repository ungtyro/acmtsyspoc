using System;
using System.Collections.Generic;
using System.Linq;
using Ung.AcmtSys.Business.Exception;

namespace Ung.AcmtSys.Business
{
    public class BankCustomer
    {
        private readonly Guid _customerId;

        public BankCustomer(AcmtSysDbEntities context,string personalCardId)
        {
            var customer = context.Customers.FirstOrDefault(x => x.PersonalCardId == personalCardId);
            if (customer == null)
            {
                throw new BankSystemException($"A customer with this Personal ID {personalCardId} is not found in system.");
            }

            _customerId = customer.CustomerId;
        }


        public Account GetAccount(AcmtSysDbEntities context, string accountNumber)
        {
            return context.Accounts.FirstOrDefault(x =>
                x.Customer.CustomerId == _customerId && x.AccountNumber == accountNumber);
        }

        public List<Account> GetAccounts(AcmtSysDbEntities context)
        {
            return context.Accounts.Where(x =>
                x.Customer.CustomerId == _customerId).ToList();
        }

        public string RequestToOpenAccount(AcmtSysDbEntities context, string accountName, BankAccountType accountType)
        {
            var masterBankAccountType = context.MasterBankAccountTypes.First(x=>x.AccountType == accountType.ToString());

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                CustomerId = _customerId,
                MasterBankAccountTypeId = masterBankAccountType.MasterBankAccountTypeId,
                AccountNumber =Guid.NewGuid().ToString(),
                AccountName = accountName,
                OpenDate = DateTime.UtcNow,
                Status = AccountStatusType.Open.ToString(),
                CurrencyCode = Currencies.USD.ToString(),
                CurrentBalance = 0m
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            return account.AccountNumber;
        }
    }
}
