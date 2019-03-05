using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Swashbuckle.AspNetCore.Swagger;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class DeviceController : BaseController<Device, DeviceDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        public DeviceController(IMapper mapper, WorkContext context) : base(mapper, context)
        {
        }

        // GET: api/Device
        /// <summary>Gets the device list.</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDevices()
        {
            return Ok(Context.Devices.Include(x => x.Employee).Select(x => Mapper.Map(x, new DeviceDTO())));
        }

        // GET: api/Device/5
        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDevice(long id)
        {
            return Ok(Context.Devices.Include(x => x.Employee).Where(x => x.Id == id).Select(x => Mapper.Map(x, new DeviceDTO())));
        }

        // GET: api/Device
        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDeviceUseHistory(DeviceDTO d)
        {
            var targetDevice = Context.Devices.Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Name == d.Name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var usages = Mapper.Map(targetDevice, new DeviceUsageListDTO());

            return Ok(usages);
        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDeviceCurrentInfo(DeviceDTO d)
        {
            var targetDevice = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == d.Name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var currentUsage = targetDevice.UsageList.Where(x => x.To == null)
                .Select(x => Mapper.Map(x, new UsageUserDTO()));

            return Ok(currentUsage);
        }
        

        // POST: api/Device
        /// <summary>
        /// Adds the device.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// Appropriate message if device was added, if it already exists or if employee wasn't found.
        /// </returns>
        [HttpPost]
        public IActionResult AddDevice(DeviceDTO d)
        {
            var device = Context.Devices.Include(x => x.UsageList).Include(x => x.Employee)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device != null) return BadRequest("Device Already Exists");
            
            var employee = Context.Employees.Include(x => x.Devices).FirstOrDefault(x => x.FirstName == d.UserFn && x.LastName == d.UserLn);

            if (employee == null) return BadRequest("Employee doesn't exist");

            var newDevice = Mapper.Map(d, new Device());
            employee.Devices.Add(newDevice);

            newDevice.UsageList.Add(new DeviceUsage
            {
                Employee = employee,
                From = DateTime.Now,
                To = null
            });
            Context.SaveChanges();
           
            return Ok("Added Device");
        }

        // PUT: api/Device/5
        /// <summary>
        /// Changes the name of the device.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeDeviceName(long id, DeviceDTO d)
        {
            var targetDevice = Context.Devices.Include(x => x.Employee).FirstOrDefault(x => x.Id == id);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            Mapper.Map(d, targetDevice);
            Context.SaveChanges();

            return Ok("Changed Device name");
        }

        /// <summary>
        /// Changes the device user.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeDeviceUser(DeviceDTO d)
        {
            var device = Context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device == null) return NotFound("Device doesn't exist");
            
            var newUser = Context.Employees.Include(x => x.Devices)
                .FirstOrDefault(x => x.FirstName == d.UserFn && x.LastName == d.UserLn);

            if (newUser == null) return BadRequest("Employee doesn't exist");

            device.Employee.UsageList.First(x => x.Device.Name == d.Name).To = DateTime.Now;

            Context.SaveChanges();

            DeviceUsage newUsage = new DeviceUsage
            {
                From = DateTime.Now,
                To = null
            };

            newUser.Devices.Add(device);
            newUser.UsageList.Add(newUsage);
            device.UsageList.Add(newUsage);
            Context.SaveChanges();

            return Ok("Changed User");
        }


        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the device.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Greska</exception>
        [HttpDelete("{id}")]
        public IActionResult DeleteDevice (long id)
        {
            return base.Delete(id);
        }
    }
}
