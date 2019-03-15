using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.UnitOfWork
{
    public class UnitOfWorkFilter : IActionFilter
    {
        private readonly WorkContext _context;

        private readonly IUnitOfWork _unitOfWork;
        
        public UnitOfWorkFilter(WorkContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method.Equals("Get")) return;

            _unitOfWork.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method.Equals("Get")) return;

            if (context.Exception == null && context.ModelState.IsValid)
            {

                try
                {
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    throw new Exception("ERROR");
                }
                
            }

            else
            {
                _context.Database.RollbackTransaction();

                throw new Exception("ERROR");
            }
        }
    }
}
