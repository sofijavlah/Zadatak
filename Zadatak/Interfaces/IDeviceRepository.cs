using System.Collections.Generic;
using Zadatak.DTOs.Device;
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
        IEnumerable<DeviceUsage> GetDeviceUseHistory(long id);

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="d">The d.</param>
        void GetDeviceCurrentInfo(DeviceDto d);
    }
}
