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
        Employee GetEmployeeUseHistory(long id);

    }
}
