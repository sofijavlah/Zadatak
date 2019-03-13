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
        Employee GetEmployeeUseHistory(long id);

    }
}
