using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Zadatak.Exceptions
{
    public class MyException
    {
        public string Message { get; set; }

        public string Exception { get; set; }

        public string StackTrace { get; set; }
        
    }
}
