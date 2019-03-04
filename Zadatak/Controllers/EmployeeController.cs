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
            var targetEmployee = context.Employees.FirstOrDefault(x => x.Id == id);

            if (targetEmployee == null) return NotFound();

            return Ok(_mapper.Map(targetEmployee, new EmployeeDTO()));
        }

        // GET: api/Employee/5
        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployeeUseHistory([FromQuery] EmployeeDTO e)
        {
            var employee =
                context.Employees.FirstOrDefault(
                    x => x.FirstName == e.FName && x.LastName == e.LName);

            if (employee == null) return NotFound("Employee doesn't exist");

            if (employee.UsageList.Count <= 0) return Ok("Employee hasn't used any device yet");

            var history = _mapper.Map<Employee, EmployeeDeviceUsageListDTO>(employee);

            return Ok(history);
        }


        /// <summary>
        /// Gets the employee office.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployeeOffice([FromQuery] EmployeeDTO e)
        {
            var targetEmployee = context.Employees.Include(x => x.Office)
                .FirstOrDefault(x => x.FirstName == e.FName && x.LastName == e.LName);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            return Ok(_mapper.Map(targetEmployee.Office, new OfficeDTO()));
        }

        /// <summary>
        /// Gets the device user.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceUser([FromQuery]DeviceDTO d)
        {
            var targetDevice = context.Devices.Include(x => x.Employee)
                .FirstOrDefault(x => x.Name == d.Name);

            if (targetDevice == null) return NotFound("Device doesn't exist");

            return Ok(_mapper.Map(targetDevice.Employee, new EmployeeDTO()));
        }

        // POST: api/Employee
        /// <summary>
        /// Adds the employee to office. If the office doesn't exists, it adds a new office
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddEmployeeToOffice([FromQuery] EmployeeDTO e)
        {
            var office = context.Offices.Include(x => x.Employees)
                .FirstOrDefault(x => x.Description == e.OfficeName);

            if (office == null)
            {
                office = new Office
                {
                    Description = e.OfficeName
                };
                context.SaveChanges();

                var newEmployee = _mapper.Map<EmployeeDTO, Employee>(e);
                office.Employees.Add(newEmployee);

                context.Offices.Add(office);
                context.SaveChanges();

                return Ok("Added Office and Employee");
            }

            office.Employees.Add(_mapper.Map(e, new Employee()));
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

            _mapper.Map(e, targetEmployee);

            context.SaveChanges();

            return Ok("Modified Employee name");
        }

        // PUT: api/Employee/5
        /// <summary>
        /// Changes the employee office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult ChangeEmployeeOffice(long id, [FromQuery] OfficeDTO o)
        {
            var targetEmployee = context.Employees.Include(x => x.Office).FirstOrDefault(x => x.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            var newOffice = context.Offices.Include(x => x.Employees)
                .FirstOrDefault(x => x.Description == o.OfficeName);

            if (newOffice == null) return BadRequest("Office doesn't exist");

            var oldOffice = targetEmployee.Office;
            
            newOffice.Employees.Add(targetEmployee);

            oldOffice.Employees.Remove(targetEmployee);

            context.SaveChanges();

            return Ok("Changed Employee Office");
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
