using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        public Device GetDeviceUseHistory(long id)
        {
            var device = Context.Devices.Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Id == id);

            return device;

        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Device GetDeviceCurrentInfo(long id)
        {
            var device = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Id == id);

            return device;
        }


    }
}
