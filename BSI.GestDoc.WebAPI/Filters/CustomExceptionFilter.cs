using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace BSI.GestDoc.WebAPI.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;
            var exceptionType = actionExecutedContext.Exception.GetType();
            CustomException.CustomException customException = null;

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Access to the Web API is not authorized.";
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                message = actionExecutedContext.Exception.Message;
                status = HttpStatusCode.BadRequest;
            }
            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = status
            };

            //Realiza o tratamento de erro para gravar log
            if (exceptionType != typeof(CustomException.CustomException))
            {
                customException =
                    new CustomException.CustomException(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
            }
            else
            {
                customException = (CustomException.CustomException)actionExecutedContext.Exception;
            }

            base.OnException(actionExecutedContext);
        }
    }
}
