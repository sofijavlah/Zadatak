using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zadatak.Attributes;
using Zadatak.Exceptions;

namespace Zadatak.Filters
{
    /// <summary>
    /// Modify the response of actions
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IResultFilter" />
    [IsFilter]
    public class MyReturnFilter : IResultFilter
    {

        /// <summary>
        /// Called before the action result executes.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext" />.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as ObjectResult;

            var _exceptionHandling = new ExceptionHandling();

            if (result.StatusCode >= 200 && result.StatusCode <= 300)
            {
                _exceptionHandling.Data = result.Value;
                _exceptionHandling.Exception = null;
                _exceptionHandling.IsError = false;
            }

            if (result.StatusCode >= 400 && result.StatusCode <= 500)
            {
                _exceptionHandling.Data = result.Value;
                _exceptionHandling.IsError = true;
                _exceptionHandling.Exception = null;
            }

            context.Result = new ObjectResult(_exceptionHandling);
        }

        /// <summary>
        /// Called after the action result executes.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutedContext" />.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

    }
}
