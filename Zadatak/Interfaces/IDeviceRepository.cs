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

        /// <summary>
        /// Adds the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        void Add(DeviceDto dto);

        /// <summary>
        /// Gets the name of the device by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Device GetDeviceByName(string name);
    }
}
