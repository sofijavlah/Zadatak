using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Exceptions
{
    public class ExceptionHandling 
    {
        public object Data { get; set; }

        public bool IsError { get; set; }

        public MyException Exception { get; set; }

        
    }
}
