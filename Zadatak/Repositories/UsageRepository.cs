using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
using Zadatak.DTOs.Device;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso>
    ///     <cref>Repositories.Repository{Models.DeviceUsage}</cref>
    /// </seealso>
    /// <seealso cref="Zadatak.Interfaces.IUsageRepository" />
    [DefineScopeType]
    public class UsageRepository : Repository<DeviceUsage>, IUsageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UsageRepository(WorkContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [NoUnitOfWork]
        public override IEnumerable<DeviceUsage> GetAll()
        {
            var usages = Context.DeviceUsages.Include(x => x.Employee).Include(x => x.Device).ToList();

            return usages;
        }

        /// <summary>
        /// Adds the specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        public void Add(Device device)
        {
            Context.DeviceUsages.Add(new DeviceUsage
            {
                From = DateTime.Now,
                To = null,
                Device = device,
                Employee = device.Employee
            });

            Context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified device usage.
        /// </summary>
        /// <param name="deviceUsage">The device usage.</param>
        public override void Update(DeviceUsage deviceUsage)
        {
            var usage = Context.DeviceUsages.Find(deviceUsage.Id);

            usage.To = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
