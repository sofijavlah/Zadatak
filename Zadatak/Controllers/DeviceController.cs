using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly WorkContext context;

        /// <summary>Initializes a new instance of the <see cref="DeviceController"/> class.</summary>
        /// <param name="contextt">The contextt.</param>
        public DeviceController(WorkContext contextt)
        {
            context = contextt;
            context.SaveChanges();
        }

        // GET: api/Device
        /// <summary>Gets the device list.</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceList()
        {
            var devices = context.Devices.Select(d => new DeviceDTO
            {
                Name = d.Name
            });

            return Ok(devices);
        }

        // GET: api/Device
        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceUseHistory(string name)
        {

            long id = context.Devices.Where(x => x.Name == name).Select(x => x.Id).First();

            var usages = context.DeviceUsages.Where(x => x.Device.Id == id).GroupBy(x => x.Device.Name).Select(x => new
            {
                Device = x.Key,
                Usages = x.Select(u =>  new UserUsageDTO
                {
                    User = u.Employee.FirstName + " " + u.Employee.LastName,
                    From = u.From,
                    To = u.To
                })
            }).OrderBy(d => d.Usages.OrderBy(f => f.From));

            return Ok(usages);


        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceCurrentInfo(string name)
        {
            var targetDevice = context.Devices.Include(d => d.Employee).Include(d => d.UsageList)
                .FirstOrDefault(d => d.Name == name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var usage = targetDevice.UsageList.Where(x => x.To == null).Select(u => new UsageDTO
            {
                From = u.From,
                To = u.To,
            }).First();

            var userInfo = new UserUsageDTO
            {
                User = targetDevice.Employee.FirstName + " " + targetDevice.Employee.LastName,
                From = usage.From,
                To = usage.To
            };

            return Ok(userInfo);
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
        public IActionResult AddDevice([FromQuery]DeviceUserInfoDTO d)
        {
            var device = context.Devices.Include(x => x.UsageList).Include(x => x.Employee).ThenInclude(x => x.Office)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device != null) return BadRequest("Device Already Exists");

            var employee = context.Employees.Include(x => x.Devices)
                .FirstOrDefault(x => x.FirstName + " " + x.LastName == d.User);

            if (employee == null) return BadRequest("Employee doesn't exist");

            var newDevice = new Device();
            context.SaveChanges();

            newDevice.Name = d.Name;
            newDevice.Employee = employee;

            var newUsage = new DeviceUsage();
            context.SaveChanges();

            newUsage.Employee = employee;
            newUsage.Device = newDevice;
            newUsage.From = DateTime.Now;
            newUsage.To = null;
           
            context.Devices.Add(newDevice);
            context.SaveChanges();

            context.DeviceUsages.Add(newUsage);
            context.SaveChanges();

            newDevice.UsageList.Add(newUsage);
            employee.UsageList.Add(newUsage);
            employee.Devices.Add(newDevice);

            //newDevice.UsageList.Add(newUsage);
            context.SaveChanges();
            context.DeviceUsages.Add(newUsage);
            //employee.Devices.Add(newDevice);
            //context.SaveChanges();
            return Ok("Added Device");
        }

        // PUT: api/Device/5
        /// <summary>
        /// Changes the name of the device.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult ChangeDeviceName(long id, [FromQuery] DeviceDTO device)
        {
            var targetDevice = context.Devices.Include(d => d.Employee).FirstOrDefault(d => d.Id == id);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            targetDevice.Name = device.Name;
            context.SaveChanges();
            return Ok("Changed Device name");
        }

        // PUT: api/Device/5
        /// <summary>
        /// Changes the device user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeDeviceUser([FromQuery]DeviceUserInfoDTO d)
        {
            var device = context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device == null) return NotFound("Device doesn't exist");

            var oldUser = device.Employee;

            var newUser = context.Employees.Include(x => x.Devices)
                .FirstOrDefault(x => x.FirstName + " " + x.LastName == d.User);

            if (newUser == null) return BadRequest("Employee doesn't exist");

            var oldUsage = oldUser.UsageList.First(x => x.Device.Name == d.Name);
            oldUsage.To = DateTime.Now;

            context.SaveChanges();
            
            device.Name = d.Name;

            DeviceUsage newDeviceUsage = new DeviceUsage
            {
                From = DateTime.Now,
                To = null
            };

            device.Employee = newUser;

            newDeviceUsage.Employee = newUser;
            newDeviceUsage.Device = device;

            device.UsageList.Add(newDeviceUsage);
            context.SaveChanges();

            newUser.Devices.Add(device);
            newUser.UsageList.Add(newDeviceUsage);
            context.DeviceUsages.Add(newDeviceUsage);

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
            var targetDevice = context.Devices.Find(id);

            if (targetDevice == null) return BadRequest("Device doesn't exist");

            context.Devices.Remove(targetDevice);
            try
            {
                context.SaveChanges();
                return Ok("Deleted");
            }

            catch (DbUpdateException ex)
            {

                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    var exNum = sqlException.Number;

                    if (exNum == 547)
                    {
                        return BadRequest("Cannot delete device from history.");
                    }
                }
                return BadRequest("I don't know");
            } 
        }
    }
}
