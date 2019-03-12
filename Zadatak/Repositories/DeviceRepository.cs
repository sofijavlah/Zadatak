using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zadatak.DTOs.Device;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// Device Repository
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.Device}</cref>
    /// </seealso>
    /// <seealso cref="Zadatak.Interfaces.IDeviceRepository" />
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DeviceRepository(WorkContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Device doesn't exist</exception>
        public IEnumerable<DeviceUsage> GetDeviceUseHistory(long id)
        {
            var device = Context.Devices.Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if (device == null) throw new Exception("Device doesn't exist");

            var usages = device.UsageList;

            return usages;

        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="d">The d.</param>
        public void GetDeviceCurrentInfo(DeviceDto d)
        {

        }

    }
}
