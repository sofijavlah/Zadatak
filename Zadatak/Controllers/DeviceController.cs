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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly WorkContext context;

        public DeviceController(IMapper mapper, WorkContext contextt)
        {
            context = contextt;
            _mapper = mapper;
        }

        // GET: api/Device
        /// <summary>Gets the device list.</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceList()
        {
            var devices = context.Devices.Select(x => _mapper.Map(x, new DeviceDTO()));

            return Ok(devices);
        }

        // GET: api/Device
        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceUseHistory([FromQuery] DeviceDTO d)
        {
            var targetDevice = context.Devices.Include(x => x.Employee).Include(x => x.UsageList).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Name == d.Name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var usages = _mapper.Map(targetDevice, new DeviceUsageListDTO());

            return Ok(usages);
        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceCurrentInfo([FromQuery] DeviceDTO d)
        {
            var targetDevice = context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == d.Name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var currentUsage = targetDevice.UsageList.Where(x => x.To == null)
                .Select(x => _mapper.Map(x, new UsageUserDTO()));

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
        public IActionResult AddDevice([FromQuery]DeviceDTO d)
        {
            var device = context.Devices.Include(x => x.UsageList).Include(x => x.Employee)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device != null) return BadRequest("Device Already Exists");
            
            var employee = context.Employees.Include(x => x.Devices).FirstOrDefault(x => x.FirstName == d.UserFn && x.LastName == d.UserLn);

            if (employee == null) return BadRequest("Employee doesn't exist");

            var newDevice = _mapper.Map(d, new Device());
            employee.Devices.Add(newDevice);

            newDevice.UsageList.Add(new DeviceUsage
            {
                Employee = employee,
                From = DateTime.Now,
                To = null
            });
            context.SaveChanges();
           
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

            _mapper.Map(device, targetDevice);
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
        public IActionResult ChangeDeviceUser([FromQuery]DeviceDTO d)
        {
            var device = context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device == null) return NotFound("Device doesn't exist");
            
            var newUser = context.Employees.Include(x => x.Devices)
                .FirstOrDefault(x => x.FirstName == d.UserFn && x.LastName == d.UserLn);

            if (newUser == null) return BadRequest("Employee doesn't exist");

            device.Employee.UsageList.First(x => x.Device.Name == d.Name).To = DateTime.Now;

            context.SaveChanges();

            DeviceUsage newUsage = new DeviceUsage
            {
                From = DateTime.Now,
                To = null
            };

            newUser.Devices.Add(device);
            newUser.UsageList.Add(newUsage);
            device.UsageList.Add(newUsage);
            context.SaveChanges();

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
