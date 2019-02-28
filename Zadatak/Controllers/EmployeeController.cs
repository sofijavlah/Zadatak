using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly WorkContext context;

        public EmployeeController(IMapper mapper, WorkContext contextt)
        {
            context = contextt;
            _mapper = mapper;
        }

        // GET: api/Employee
        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = context.Offices.Include(x => x.Employees)
                .Select(x => _mapper.Map(x, new OfficeEmployeeListDTO()));
            return Ok(employees);
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            var targetEmployee = context.Employees.Include(e => e.Office).FirstOrDefault(x => x.Id == id);

            if (targetEmployee == null) return NotFound();

            return Ok(_mapper.Map(targetEmployee, new EmployeeDTO()));
        }

        // GET: api/Employee/5
        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{fName}/{lName}")]
        public IActionResult GetEmployeeUseHistory(string fName, string lName)
        {
            var targetEmployee = context.Employees.Include(e => e.Office).Include(e => e.Devices)
                .FirstOrDefault(x => x.FirstName == fName && x.LastName == lName);

            if (targetEmployee == null) return NotFound();


            var employee = new EmployeeOfficeInfoDeviceListDTO
            {
                FName = targetEmployee.FirstName,
                LName = targetEmployee.LastName,
                OfficeName = targetEmployee.Office.Description,
                DeviceUsageList = targetEmployee.UsageList.Select(x => new DeviceUsageInfoDTO()
                {
                    Name = x.Device.Name,
                    From = x.From,
                    To = x.To
                })
            };

            return Ok(employee);
        }

        /// <summary>
        /// Gets the employee office.
        /// </summary>
        /// <param name="fName">Name of the f.</param>
        /// <param name="lName">Name of the l.</param>
        /// <returns></returns>
        [HttpGet("{fName}/{lName}")]
        public IActionResult GetEmployeeOffice(string fName, string lName)
        {
            var targetEmployee = context.Employees.Include(e => e.Office)
                .FirstOrDefault(x => x.FirstName == fName && x.LastName == lName);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            OfficeDTO office = new OfficeDTO
            {
                OfficeName = targetEmployee.Office.Description
            };

            return Ok(office);
        }

        /// <summary>
        /// Gets the device user.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceUser([FromQuery]DeviceDTO device)
        {
            var targetDevice = context.Devices.Include(x => x.Employee)
                .FirstOrDefault(x => x.Name == device.Name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            var targetEmployee = new EmployeeDTO
            {
                FName = targetDevice.Employee.FirstName,
                LName = targetDevice.Employee.LastName
            };

            return Ok(targetEmployee);
        }

        // POST: api/Employee
        /// <summary>
        /// Adds the employee to office.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddEmployeeToOffice([FromQuery] EmployeeOfficeInfoDTO e)
        {
            var broj = context.Offices.Count(o => o.Description == e.OfficeName);

            if (broj == 0)
            {
                Office o = new Office();
                o.Description = e.OfficeName;

                context.Offices.Add(o);

                context.SaveChanges();

                Employee newEmployee = new Employee();
                newEmployee.FirstName = e.FName;
                newEmployee.LastName = e.LName;
                newEmployee.Office = o;

                context.Employees.Add(newEmployee);

                context.SaveChanges();

                return Ok("Added Employee and Office");
            }

            var office = context.Offices.Include(o => o.Employees).First(o => o.Description == e.OfficeName);

            Employee employee = new Employee();
            employee.FirstName = e.FName;
            employee.LastName = e.LName;
            employee.OfficeId = office.Id;

            context.Employees.Add(employee);
            context.SaveChanges();

            return Ok("Added Employee");
        }

        // PUT: api/Employee/5
        /// <summary>
        /// Changes the name of the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult ChangeEmployeeName(long id, [FromQuery]EmployeeDTO e)
        {

            var targetEmployee = context.Employees.Include(em => em.Office).FirstOrDefault(em => em.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            targetEmployee.FirstName = e.FName;
            targetEmployee.LastName = e.LName;

            context.SaveChanges();

            return Ok("Modified Employee name");
        }

        

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            
            var targetEmployee = context.Employees.FirstOrDefault(e => e.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            context.Employees.Remove(targetEmployee);

            try
            {
                context.SaveChanges();
            }

            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException() is SqlException sqlException)
                {
                    var exNum = sqlException.Number;

                    if (exNum == 547)
                    {
                        return BadRequest("Employee cannot be deleted from history");
                    }
                }
            }

            return Ok("Deleted");
        }
    }
}
