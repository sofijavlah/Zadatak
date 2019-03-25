using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Zadatak.Attributes;
using Zadatak.Exceptions;

namespace Zadatak.Filters
{
    /// <summary>
    /// Handle exceptions
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter" />
    [IsFilter]
    public class MyExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var _exceptionHandling = new ExceptionHandling {Data = null, IsError = true};
            
            MyException ex = new MyException
            {
                Message = context.Exception.Message,
                Exception = context.Exception.Data.ToString(),
                StackTrace = context.Exception.StackTrace
            };

            _exceptionHandling.Exception = ex;

            context.Result = new ObjectResult(_exceptionHandling);
        }
    }
}
