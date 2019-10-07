using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Ung.AcmtSys.Business.Exception;

namespace Ung.AcmtSys.Service.ExceptionHelper
{
    public  class ExceptionHandler
    {
        public void ModelStateExceptions(ModelStateDictionary modelState)
        {
            var content = string.Join(",", modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)).ToList());
            var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(string.Format(content))

            };
            throw new HttpResponseException(resp);
        }
    }

    public class CustomException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BankSystemException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}