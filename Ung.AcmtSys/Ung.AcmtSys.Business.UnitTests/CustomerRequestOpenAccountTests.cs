using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ung.AcmtSys.Business.Exception;

namespace Ung.AcmtSys.Business.UnitTests
{
    [TestClass]
    public class CustomerRequestOpenAccountTests
    {
        private AcmtSysDbEntities _context;
        private Bank _bank;
        [TestInitialize]
        public void InitialTest()
        {
            _context = new AcmtSysDbEntities();
            _bank = new Bank();

        }

        [TestMethod]
        public void TestCustomerRequestOpenAccount()
        {
            //create new customer (A)
            #region create A Account
            var newCustomer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Prefix = "Mr",
                FirstName = $"FirstName{DateTime.UtcNow.Ticks}",
                LastName = $"LastName{DateTime.UtcNow.Ticks}",
                Sex = Sex.M.ToString(),
                PersonalCardId = Guid.NewGuid().ToString(),
                Birthdate = 19890828
            };

            try
            {
                _bank.AddPersonalCustomer(_context, newCustomer);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            var customer = _bank.GetCustomer(_context, newCustomer.PersonalCardId);
            Assert.AreEqual(newCustomer.CustomerId, customer.CustomerId);

            var bankCustomer = new BankCustomer(_context, newCustomer.PersonalCardId);

            //check current account of new customer
            var account = bankCustomer.GetAccount(_context, string.Empty);
            Assert.AreEqual(null, account);
            var accounts = bankCustomer.GetAccounts(_context);
            Assert.AreEqual(0, accounts.Count);
            #endregion

            //request to open new account
            var accountNumber = bankCustomer.RequestToOpenAccount(_context, $"AccountName{DateTime.UtcNow.Ticks}", BankAccountType.ST);
            account = bankCustomer.GetAccount(_context, accountNumber);
            Assert.AreEqual(accountNumber, account.AccountNumber);
            accounts = bankCustomer.GetAccounts(_context);
            Assert.AreEqual(1, accounts.Count);

            //deposit 1000 USD to account and calculate 0.1% charge amount
            var depositAmount = 1000m;
            var savingAccount = new BankSavingAccount(_context, accountNumber);
                //BankAccountFactory.CreateInstanceAccount(_context, accountNumber);
            savingAccount.DepositMoney(_context, depositAmount);
            account = bankCustomer.GetAccount(_context, accountNumber);
            Assert.AreEqual(999, account.CurrentBalance);

            //withdraw 99 USD
            var withdrawAmount = 99m;
            savingAccount.WithdrawMoney(_context, withdrawAmount);
            account = bankCustomer.GetAccount(_context, accountNumber);
            Assert.AreEqual(900, account.CurrentBalance);

            //create new account (B)
            #region create B Account
            var newCustomerB = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Prefix = "Mr",
                FirstName = $"FirstName{DateTime.UtcNow.Ticks}",
                LastName = $"LastName{DateTime.UtcNow.Ticks}",
                Sex = Sex.M.ToString(),
                PersonalCardId = Guid.NewGuid().ToString(),
                Birthdate = 19890828
            };

            try
            {
                _bank.AddPersonalCustomer(_context, newCustomerB);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            var customerB = _bank.GetCustomer(_context, newCustomerB.PersonalCardId);
            Assert.AreEqual(newCustomerB.CustomerId, customerB.CustomerId);

            var bankCustomerB = new BankCustomer(_context, newCustomerB.PersonalCardId);

            //check current account of new customer
            var accountB = bankCustomerB.GetAccount(_context, string.Empty);
            Assert.AreEqual(null, accountB);
            var accountsB = bankCustomerB.GetAccounts(_context);
            Assert.AreEqual(0, accountsB.Count);


            //request to open new account
            var accountNumberB = bankCustomerB.RequestToOpenAccount(_context, $"AccountName{DateTime.UtcNow.Ticks}", BankAccountType.ST);
            accountB = bankCustomerB.GetAccount(_context, accountNumberB);
            Assert.AreEqual(accountNumberB, accountB.AccountNumber);
            accountsB = bankCustomerB.GetAccounts(_context);
            Assert.AreEqual(1, accountsB.Count);
            #endregion


            //test transfer money from A account to B account
            savingAccount.TransferMoney(_context, accountNumberB, 100m);
            account = bankCustomer.GetAccount(_context, accountNumber);
            Assert.AreEqual(800, account.CurrentBalance);

            accountB = bankCustomerB.GetAccount(_context, accountNumberB);
            Assert.AreEqual(100, accountB.CurrentBalance);


            //withdraw over limit
            try
            {
                savingAccount.WithdrawMoney(_context, 1000m);
                Assert.Fail("Insufficient funds should be thrown");
            }
            catch (BankSystemException)
            {
            }


            //check customer exist
            var isCustomerAlreadyExist = _bank.IsCustomerAlreadyExist(_context, newCustomer.PersonalCardId);
            Assert.IsTrue(isCustomerAlreadyExist);

        }

        [TestMethod]
        [ExpectedException(typeof(BankSystemException))]
        public void TestCustomerNotFound()
        {

            var bankCustomer = new BankCustomer(_context, Guid.NewGuid().ToString());
            Assert.Fail("ArgumentException should be thrown");


        }

        [TestMethod]
        [ExpectedException(typeof(BankSystemException))]
        public void TestBankSavingAccountNotFound()
        {
            var bankSavingAccount = new BankSavingAccount(_context, Guid.NewGuid().ToString());
            Assert.Fail("ArgumentException should be thrown");
        }
    }
}
