using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.Repositories;

namespace Zadatak.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<DeviceUsage> GetEmployeeUseHistory(long id);

    }
}
