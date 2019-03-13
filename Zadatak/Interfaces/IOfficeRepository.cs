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
        Office GetOffice(string description);
    }
}
