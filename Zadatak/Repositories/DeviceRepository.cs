using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zadatak.DTOs;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        protected readonly WorkContext _context;

        public DeviceRepository(WorkContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<DeviceUsage> GetDeviceUseHistory(long id)
        {
            var device = _context.Devices.Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if (device == null) throw new Exception("Device doesn't exist");

            var usages = device.UsageList;

            return usages;

        }

        public Device GetDeviceCurrentInfo(DeviceDTO d)
        {

        }

    }
}
