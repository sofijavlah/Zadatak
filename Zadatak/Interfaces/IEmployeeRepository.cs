using System.Collections.Generic;
using Zadatak.Models;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Interface for Employee Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.IRepository{Models.Employee}</cref>
    /// </seealso>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<DeviceUsage> GetEmployeeUseHistory(long id);

        /// <summary>
        /// Gets the office.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        IEnumerable<Employee> GetOffice(string description);
    }
}
