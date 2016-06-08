using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using Memoirs.Common;

namespace Memoirs.Web2.Filters
{
    public class ErrorFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        public ErrorFilter(ILogger logger)
        {
            _logger = logger;
        }

        public bool AllowMultiple { get; }
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var apiException = actionExecutedContext.Exception as ApiException;
            if (apiException != null)
            {
                if (!string.IsNullOrEmpty(apiException.ExplicitDescription))
                {
                    _logger.Error(apiException, apiException.ExplicitDescription);
                }
                else
                {
                    _logger.Error(apiException);
                }
            }
            else
            {
                _logger.Fatal(actionExecutedContext.Exception);
            }
            return Task.FromResult<object>(null);
        }
    }
}