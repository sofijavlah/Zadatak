﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zadatak.Attributes;
using Zadatak.DTOs.Device;
using Zadatak.Exceptions;
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
    [DefineScopeType]
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
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [NoUnitOfWork]
        public override IEnumerable<Device> GetAll()
        {
            var devices = Context.Devices.Include(x => x.Employee).ToList();

            return devices;
        }

        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Doesn't exist</exception>
        [NoUnitOfWork]
        public Device GetDeviceUseHistory(long id)
        {
            var device = Context.Devices.Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if(device == null) throw new CustomException("Doesn't exist");

            return device;

        }
        
        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Doesn't exist</exception>
        [NoUnitOfWork]
        public Device GetDeviceCurrentInfo(long id)
        {
            var device = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Id == id);

            if (device == null) throw new CustomException("Doesn't exist");

            return device;
        }

        /// <summary>
        /// Gets the name of the device by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [NoUnitOfWork]
        public Device GetDeviceByName(string name)
        {
            var device = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == name);

            return device;
        }

        /// <summary>
        /// Adds the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void Add(DeviceDto dto)
        {
            var user = Context.Employees.Find(dto.Employee.EmployeeId);

            Context.Devices.Add(new Device
            {
                Name = dto.Name,
                Employee = user
            });

            Context.SaveChanges();
        }

        /// <summary>
        /// Changes the device name or user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <exception cref="Exception">Device doesn't exist</exception>
        public void ChangeDeviceNameOrUser(long id, DeviceDto dto)
        {
            var device = Context.Devices.Include(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if (device == null) throw new CustomException("Device doesn't exist");

            if (dto.Name != null && dto.Name != device.Name)
            {
                device.Name = dto.Name;
            }

            var newUser = Context.Employees.Find(dto.Employee.EmployeeId);

            device.Employee = newUser;

            Context.SaveChanges();
        }
        
        
    }
}
