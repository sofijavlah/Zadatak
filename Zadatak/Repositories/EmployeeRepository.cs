﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// Employee Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.Employee}</cref>
    /// </seealso>
    /// <seealso cref="Zadatak.Interfaces.IEmployeeRepository" />
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmployeeRepository(WorkContext context)
            : base(context)
        {
        }

        [NoUnitOfWork]
        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Employee GetEmployeeUseHistory(long id)
        {
            var employee = Context.Employees.Include(x => x.UsageList).ThenInclude(x => x.Device).FirstOrDefault(x => x.Id == id);

            return employee;
        }
    }
}
