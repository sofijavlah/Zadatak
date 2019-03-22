using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zadatak.Expressions;

namespace Zadatak.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class ExpressionsController : ControllerBase
    {
        /// <summary>
        /// Filters the input.
        /// </summary>
        /// <param name="querryInfo">The querry information.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult QuerySmor(QueryInfo querryInfo)
        {


            return Ok();
        }
    }
}
