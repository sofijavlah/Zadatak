using Zadatak.DTOs.Device;
using Zadatak.Models;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Interface for Usage Repository
    /// </summary>
    ///
    ///<seealso>
    ///     <cref>Repositories.IRepository{Models.DeviceUsage}</cref>
    /// </seealso>
    public interface IUsageRepository : IRepository<DeviceUsage>
    {
        /// <summary>
        /// Adds the specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        void Add(Device device);
    }
}
