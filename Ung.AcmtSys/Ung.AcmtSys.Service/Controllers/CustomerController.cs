using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Ung.AcmtSys.Service.Models;

namespace Ung.AcmtSys.Service.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/customer")]
    [EnableCors("*", "*", "*")]
    public class CustomerController : ApiController
    {
        private readonly ExceptionHelper.ExceptionHandler _exceptionHandler;
        public CustomerController()
        {
            _exceptionHandler = new ExceptionHelper.ExceptionHandler();
        }

        [HttpGet]
        [Route("{personalId}")]
        public async Task<CustomerModel> GetCustomer(string personalId)
        {
            //var customerManager = new CustomerManager();
            CustomerModel customer = null;
            try
            {
                //customer = customerManager.GetCustomer(personalId);
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message + ex.StackTrace)

                };
                throw new HttpResponseException(resp);
            }
            return new CustomerModel();
        }

        [HttpPost]
        [Route("new")]
        public async Task<CustomerModel> CreateNewCustomer(CreateCustomerBindigModel model)
        {
            //1.validate parameters
            if (!ModelState.IsValid)
            {
                _exceptionHandler.ModelStateExceptions(ModelState);
            }

            ////var customerManager = new CustomerManager();
            //customerManager.CreateCustomer(model.BranchId, model.Customer);
            //var customer = customerManager.GetCustomer(model.Customer.PersonalId);
            return new CustomerModel();
        }
    }
}
