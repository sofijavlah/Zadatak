using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class ExceptionOnDelete : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionOnDelete"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ExceptionOnDelete(string message)
        {
            
        }
    }
}
