using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Memoirs.Common;
using Memoirs.Web.Models;

namespace Memoirs.Web.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public bool AllowMultiple { get; }
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception as MemoirsExceptionBase;
            if (exception != null)
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorModel()
                    {
                        ErrorCode = ((MemoirsExceptionBase) actionExecutedContext.Exception).ErrorCode,
                        Message = !string.IsNullOrEmpty(exception.ErrorMessage)
                            ? exception.ErrorMessage
                            : "error occured"
                    });

            }
            else
            {
                //TODO:log it!
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorModel()
                    {
                        ErrorCode = -1,
                        Message = "ERROR"
                    });
            }
            

            return Task.FromResult<object>(null);
        }
    }
}