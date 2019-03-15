using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Zadatak.Exceptions;
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
                
                catch (DbUpdateException ex)
                {
                    if (ex.GetBaseException() is SqlException sqlException)
                    {
                        var exNum = sqlException.Number;
                        if (exNum == 547) throw new CustomException("Cannot delete"); ////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    }
                }

                catch (CustomException e)
                {
                    throw new CustomException("MY_ERROR!");
                }



            }

            else
            {
                _context.Database.RollbackTransaction();

                throw new CustomException("ERROR");
            }
        }
    }
}
