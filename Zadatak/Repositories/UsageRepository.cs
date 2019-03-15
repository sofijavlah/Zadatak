using System;
using Microsoft.EntityFrameworkCore;
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
        /// Adds the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
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
