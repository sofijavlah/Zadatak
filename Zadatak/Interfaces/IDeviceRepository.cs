using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.Repositories;

namespace Zadatak.Interfaces
{
    public interface IDeviceRepository : IRepository<Device>
    {
        IEnumerable<DeviceUsage> GetDeviceUseHistory(long id);

        Device GetDeviceCurrentInfo(DeviceDTO d);
    }
}
