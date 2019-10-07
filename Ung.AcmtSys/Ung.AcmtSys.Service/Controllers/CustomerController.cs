using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Ung.AcmtSys.Business;
using Ung.AcmtSys.Service.ExceptionHelper;
using Ung.AcmtSys.Service.Models;

namespace Ung.AcmtSys.Service.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/customers")]
    [EnableCors("*", "*", "*")]
    [CustomException]
    public class CustomerController : ApiController
    {
        private readonly ExceptionHelper.ExceptionHandler _exceptionHandler;
        public CustomerController()
        {
            _exceptionHandler = new ExceptionHelper.ExceptionHandler();
        }

        /// <summary>
        /// Provide customer personal information.
        /// </summary>
        /// <param name="personalCardId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{personalCardId:maxlength(50)}")]
        public CustomerModel GetCustomer(string personalCardId)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            CustomerModel customerModel = null;

            var context = new AcmtSysDbEntities();
            var bank = new Bank();
            var customerObject = bank.GetCustomer(context, personalCardId);

            if (customerObject != null)
            {
                customerModel = new CustomerModel(customerObject);
            }

            return customerModel;
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateNewCustomer(CreateCustomerBindingModel model)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var bank = new Bank();
            bank.AddPersonalCustomer(context, model.Customer.CreateNewCustomerObject());

            return Ok();
        }


        /// <summary>
        /// Check if the Personal ID card is in the system or not.
        /// </summary>
        /// <param name="personalCardId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("verification/{personalCardId:maxlength(50)}")]
        public bool CheckPersonalCardIdDuplicate(string personalCardId)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var bank = new Bank();
            var isAlreadyExist = bank.IsCustomerAlreadyExist(context, personalCardId);

            return isAlreadyExist;
        }

        /// <summary>
        /// Provide all bank account information of the desired client.
        /// </summary>
        /// <param name="personalCardId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{personalCardId:maxlength(50)}/accounts")]
        public List<CustomerAccount> GetAllCustomerAccounts(string personalCardId)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var bankCustomer = new BankCustomer(context, personalCardId);

            List<CustomerAccount> customerAccounts = null;

            var accountObjects = bankCustomer.GetAccounts(context);
            if (accountObjects != null)
            {
                customerAccounts = new List<CustomerAccount>();
                foreach (var accountObject in accountObjects)
                {
                    customerAccounts.Add(new CustomerAccount(accountObject));
                }
            }

            return customerAccounts;
        }

        /// <summary>
        /// Provide bank account information of the desired client.
        /// </summary>
        /// <param name="personalCardId"></param>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{personalCardId:maxlength(50)}/accounts/{accountNumber:maxlength(50)}")]
        public CustomerAccount GetCustomerAccount(string personalCardId,string accountNumber)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var bankCustomer = new BankCustomer(context, personalCardId);

            CustomerAccount customerAccount = null;

            var accountObject = bankCustomer.GetAccount(context, accountNumber);
            if (accountObject != null)
            {
                customerAccount = new CustomerAccount(accountObject);
            }

            return customerAccount;
        }

        /// <summary>
        /// Open saving account.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("accounts")]
        public string RequestOpenSavingAccount(RequestOpenSavingAccount model)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var bankCustomer = new BankCustomer(context, model.PersonalCardId);
            var accountNumber = bankCustomer.RequestToOpenAccount(context, model.AccountName, BankAccountType.ST);

            return accountNumber;
        }
    }
}
