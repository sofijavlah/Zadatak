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
        Device GetDeviceUseHistory(long id);

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Device GetDeviceCurrentInfo(long id);

        /// <summary>
        /// Changes the device name or user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        void ChangeDeviceNameOrUser(long id, DeviceDto dto);

        void Add(DeviceDto dto);

        Device GetDeviceByName(string name);
    }
}
