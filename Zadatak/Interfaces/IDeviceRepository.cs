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

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Device GetDeviceCurrentInfo(long id);
    }
}
