using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
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
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDesc == null) return;

            bool disable = Attribute.IsDefined(actionDesc.MethodInfo, typeof(NoUnitOfWork));

            _unitOfWork.Start(disable);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (_unitOfWork.GetReadOnly()) return;

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
                    throw e;
                }
            }
            else
            {
                _context.Database.RollbackTransaction();

                throw context.Exception;
            }
        }
    }
}
