using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Zadatak.Exceptions;

namespace Zadatak.Filters
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var _exceptionHandling = new ExceptionHandling();

            _exceptionHandling.Data = null;

            _exceptionHandling.IsError = true;

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
