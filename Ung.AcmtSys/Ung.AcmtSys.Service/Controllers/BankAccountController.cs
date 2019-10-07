using System.Web.Http;
using System.Web.Http.Cors;
using Ung.AcmtSys.Business;
using Ung.AcmtSys.Service.ExceptionHelper;
using Ung.AcmtSys.Service.Models;

namespace Ung.AcmtSys.Service.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/bankaccount")]
    [EnableCors("*", "*", "*")]
    [CustomException]
    public class BankAccountController : ApiController
    {
        private readonly ExceptionHandler _exceptionHandler;
        public BankAccountController()
        {
            _exceptionHandler = new ExceptionHandler();
        }

        /// <summary>
        ///  Deposit money into the desired account.
        /// </summary>
        [HttpPost]
        [Route("deposit")]
        public IHttpActionResult Deposit(DepositBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var savingBankAccount = new BankSavingAccount(context, model.AccountNumber);
            savingBankAccount.DepositMoney(context, model.DepositAmount);

            return Ok();

        }

        /// <summary>
        ///  Transfer money from source account to destination account
        /// </summary>
        [HttpPost]
        [Route("direct-transfer")]
        public IHttpActionResult TransferAmountInternal(DirectTransferBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            var context = new AcmtSysDbEntities();
            var savingBankAccount = new BankSavingAccount(context, model.SourceAccountNumber);
            savingBankAccount.TransferMoney(context, model.DestinationAccountNumber, model.TransferAmount);

            return Ok();

        }
    }
}
