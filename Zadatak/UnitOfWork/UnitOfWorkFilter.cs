using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.UnitOfWork
{
    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        private readonly WorkContext _context;

        private readonly IUnitOfWork _unitOfWork;
        
        public UnitOfWorkFilter(WorkContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Get")) return;

            _unitOfWork.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;

            if (context.Exception == null && context.ModelState.IsValid)
            {
                _unitOfWork.Commit();
            }

            else
            {
                _context.Database.RollbackTransaction();
            }
        }
    }
}
