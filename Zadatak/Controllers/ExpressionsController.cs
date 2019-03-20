using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zadatak.Expressions;

namespace Zadatak.Controllers
{
    public class ExpressionsController : ControllerBase
    {
        [HttpPost]
        public IActionResult FilterInput(QueryInfo querryInfo)
        {
            return Ok();
        }
    }
}
