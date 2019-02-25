using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly WorkContext context;

        public DeviceController(WorkContext contextt)
        {
            context = contextt;
            context.SaveChanges();
        }

        // GET: api/Device
        [HttpGet]
        public IActionResult GetDeviceList()
        {
            var devices = context.Devices.Select(d => new DeviceDTO
            {
                Name = d.Name
            });

            return Ok(devices);
        }

        // GET: api/DeviceWithDetails
        [HttpGet]
        public IActionResult GetDeviceDetails(long id)
        {
            var targetDevice = context.Devices.Include(d => d.Employee).Include(d => d.UsageList)
                .FirstOrDefault(d => d.Id == id);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var usage = targetDevice.UsageList.Select(u => new UsageDTO
            {
                From = u.From,
                To = u.To,
            });

            var device = new DeviceUsageUserInfoDTO
            {
                Name = targetDevice.Name,
                User = targetDevice.Employee.FirstName + " " + targetDevice.Employee.LastName,
                ListOfUses = usage
            };

            return Ok(device);
        }

        // GET: api/Device
        [HttpGet]
        public IActionResult GetDeviceUseHistory()
        {

           return Ok();
        }

        // GET: api/Device/5
        [HttpGet("{id}")]
        public IActionResult GetDevice(long id)
        {
            
            return Ok();
        }

        // POST: api/Device
        [HttpPost]
        public IActionResult AddDevice(DeviceUserInfoDTO d)
        {
            var device = context.Devices.Include(x => x.UsageList).Include(x => x.Employee).ThenInclude(x => x.Office)
                .FirstOrDefault(x => x.Name == d.Name);

            if (device != null) return BadRequest("Device Already Exists");

            var employee = context.Employees.Include(x => x.Devices).ThenInclude(x => x.UsageList)
                .FirstOrDefault(x => x.FirstName + " " + x.LastName == d.User);

            if (employee == null) return BadRequest("Employee doesn't exist");

            device = new Device();
            device.Name = d.Name;
            device.UsageList.Add(new DeviceUsage
            {
                From = DateTime.Now,
                To = null
            });
            employee.Devices.Add(device);
            context.SaveChanges();
            return Ok();
        }

        // PUT: api/Device/5
        [HttpPut("{id}")]
        public IActionResult ChangeDeviceName(long id, DeviceDTO device)
        {
            var targetDevice = context.Devices.Include(d => d.Employee).FirstOrDefault(d => d.Id == id);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            targetDevice.Name = device.Name;
            context.SaveChanges();
            return Ok();
        }

        // PUT: api/Device/5
        [HttpPut("{id}")]
        public IActionResult ChangeDeviceUser(long id, DeviceUserInfoDTO d)
        {
            var device = context.Devices.Include(x => x.Employee).Include(x => x.UsageList)
                .FirstOrDefault(x => x.Id == id);

            if (device == null) return NotFound("Device doesn't exist");

            var oldUser = device.Employee;

            var newUser = context.Employees.Include(x => x.Devices).ThenInclude(x => x.UsageList)
                .FirstOrDefault(x => x.FirstName + " " + x.LastName == d.User);

            if (newUser == null) return BadRequest("Employee doesn't exist");

            var oldUsage = oldUser.UsageList.First(x => x.Device.Name == d.Name);
            oldUsage.To = DateTime.Now;

            context.SaveChanges();

            newUser.Devices.Add(device);
            device.Name = d.Name;
            device.UsageList.Add(new DeviceUsage
            {
                From = DateTime.Now,
                To = null
            });
            context.SaveChanges();

            return Ok("Changed User");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDevice (long id)
        {
            return Ok();
        }
    }
}
