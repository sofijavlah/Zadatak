using Zadatak.Models;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Interface for Office Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.IRepository{Models.Office}</cref>
    /// </seealso>
    public interface IOfficeRepository : IRepository<Office>
    {
        /// <summary>
        /// Gets the office with given name
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        Office GetOffice(string description);
    }
}
