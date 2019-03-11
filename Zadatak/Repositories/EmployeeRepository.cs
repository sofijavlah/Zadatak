using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        protected readonly WorkContext _context;

        public EmployeeRepository(WorkContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<DeviceUsage> GetEmployeeUseHistory(long id)
        {
            var employee = _context.Employees.Include(x => x.UsageList).ThenInclude(x => x.Device)
                .FirstOrDefault(x => x.Id == id);

            if (employee == null) throw new Exception("Employee doesn't exist");

            var usages = employee.UsageList;

            return usages;
        }
    }
}
