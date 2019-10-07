using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Ung.AcmtSys.Service.ExceptionHelper
{
    public  class ExceptionHandler
    {
        public ExceptionHandler()
        {

        }
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
}