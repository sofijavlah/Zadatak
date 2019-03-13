using Zadatak.Models;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Interface for Device Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.IRepository{Models.Device}</cref>
    /// </seealso>
    public interface IDeviceRepository : IRepository<Device>
    {
        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Device GetDeviceUseHistory(long id);

        Device GetDeviceCurrentInfo(long id);
    }
}
