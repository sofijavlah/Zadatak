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
    [ApiController]
    public class EmployeeController : BaseController<Employee, EmployeeDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        public EmployeeController(IMapper mapper, WorkContext context) : base (mapper, context)
        {
        }

        // GET: api/Employee
        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(Context.Employees.Include(x => x.Office).Select(x => Mapper.Map(x, new EmployeeDTO())));
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployee(long id)
        {
            return Ok(Context.Employees.Include(x => x.Office).Where(x => x.Id == id).Select(x => Mapper.Map(x, new EmployeeDTO())));
        }

        // GET: api/Employee/5
        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetEmployeeUseHistory(EmployeeDTO e)
        {
            var employee = Context.Employees.Include(x => x.UsageList).ThenInclude(x => x.Device).FirstOrDefault(x => x.FirstName == e.FName && x.LastName == e.LName);

            if (employee == null) return BadRequest("Employee doesn't exist");

            if (!employee.UsageList.Any()) return Ok("Employee hasn't used any device yet");

            var history = Mapper.Map<Employee, EmployeeDeviceUsageListDTO>(employee);

            return Ok(history);
        }


        /// <summary>
        /// Gets the employee office.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetEmployeeOffice(EmployeeDTO e)
        {
            var targetEmployee = Context.Employees.Include(x => x.Office)
                .FirstOrDefault(x => x.FirstName == e.FName && x.LastName == e.LName);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            return Ok(Mapper.Map(targetEmployee.Office, new OfficeDTO()));
        }

        // POST: api/Employee
        ///// <summary>
        /// Posts the employee.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        //[HttpPost]
        //public IActionResult PostEmployee(EmployeeDTO e)
        //{
        //var office = Context.Offices.Include(x => x.Employees)
        //    .FirstOrDefault(x => x.Description == e.Office.Description);

        //if (office == null)
        //{
        //    office = new Office
        //    {
        //        Description = e.Office.Description
        //    };
        //    Context.SaveChanges();

        //    var newEmployee = Mapper.Map<EmployeeDTO, Employee>(e);
        //    office.Employees.Add(newEmployee);

        //    Context.Offices.Add(office);
        //    Context.SaveChanges();

        //    return Ok("Added Office and Employee");
        //}

        //office.Employees.Add(Mapper.Map(e, new Employee()));
        //Context.SaveChanges();

        //return Ok("Added Employee");
        //}

        // POST: api/Employee
        [HttpPost]
        public IActionResult PostEmployee(EmployeeDTO e)
        {
            return base.Post(e);
        }

        // PUT: api/Employee/5
        /// <summary>
        /// Changes the name of the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeEmployeeName(long id, EmployeeDTO e)
        {
            return Ok(base.Put(id, e));
        }

        // PUT: api/Employee/5
        /// <summary>
        /// Changes the employee office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeEmployeeOffice(long id, OfficeDTO o)
        {
            var targetEmployee = Context.Employees.Include(x => x.Office).FirstOrDefault(x => x.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            var newOffice = Context.Offices.Include(x => x.Employees)
                .FirstOrDefault(x => x.Description == o.OfficeName);

            if (newOffice == null) return BadRequest("Office doesn't exist");

            targetEmployee.Office = newOffice;

            Context.SaveChanges();

            return Ok("Changed Employee Office");
        }


        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteEmployee(long id)
        {
            return base.Delete(id);
        }
    }
}
