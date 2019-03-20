using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
using Zadatak.Exceptions;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Filters
{
    /// <summary>
    /// Unit of work to handle transactions
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
    [IsFilter]
    public class UnitOfWorkFilter : IActionFilter
    {
        private readonly WorkContext _context;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkFilter"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UnitOfWorkFilter(WorkContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDesc == null) return;

            bool disable = Attribute.IsDefined(actionDesc.MethodInfo, typeof(NoUnitOfWork));

            _unitOfWork.Start(disable);
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        /// <exception cref="CustomException">Cannot delete</exception>
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
