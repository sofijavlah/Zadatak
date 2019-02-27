using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Clauses;
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
    public class OfficeController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly WorkContext context;

        public OfficeController(IMapper mapper, WorkContext contextt)
        {
            context = contextt;
            _mapper = mapper;
        }

        // GET: api/Office
        /// <summary>
        /// Gets the offices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOffices()
        {
            var offices = context.Offices.Select(o => new OfficeDTO
            {
                OfficeName = o.Description
            });
            return Ok(offices);
        }

        // GET: api/Office/5
        /// <summary>
        /// Gets the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetOffice(long id)
        {
            //if (context.Offices.Include(o => o.Employees).FirstOrDefault(o => o.Id == id) == null)
            //{
            //    return NotFound("Office doesn't exist");
            //}

            //var targetOffice = context.Offices.Include(o => o.Employees).Where(o => o.Id == id).Select(l => new OfficeEmployeeListDTO
            //{
            //    OfficeName = l.Description,
            //    EmployeeList = l.Employees.Select(e => new EmployeeDTO
            //    {
            //        FName = e.FirstName,
            //        LName = e.LastName
            //    })
            //});
            //return Ok(targetOffice);
            var targetOffice = context.Offices.Include(x => x.Employees).FirstOrDefault(x => x.Id == id);
            
            var officeInfo = _mapper.Map<Office, OfficeEmployeeListDTO>(targetOffice);

            return Ok(officeInfo);
        }

        // POST: api/Office 
        /// <summary>
        /// Adds the office.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOffice([FromQuery]OfficeDTO o)
        {
            if(context.Offices.Count(of => of.Description == o.OfficeName) == 1) return BadRequest("Office Already Exists");
            
            Office office = new Office();
            office.Description = o.OfficeName;

            context.Offices.Add(office);
            context.SaveChanges();

            return Ok("Added");
        }

        // POST: api/Office 
        /// <summary>
        /// Adds the office and employees.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOfficeAndEmployees([FromQuery]OfficeEmployeeListDTO o)
        {
            if (context.Offices.Count(of => of.Description == o.OfficeName) > 0) return BadRequest("Office Already Exists");

            Office office = new Office();
            office.Description = o.OfficeName;

            office.Employees = new List<Employee>();
            foreach (EmployeeDTO employee in o.Employees)
            {
                office.Employees.Add(new Employee
                {
                    FirstName = employee.FName,
                    LastName = employee.LName
                });
            }

            context.Offices.Add(office);
            context.SaveChanges();

            return Ok("Added");
        }

        // PUT: api/Office/5
        /// <summary>
        /// Changes the name of the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult ChangeOfficeName(long id, [FromQuery] OfficeDTO o)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            var targetOffice = context.Offices.Where(of => of.Id == id).Include(of => of.Employees).FirstOrDefault();

            targetOffice.Description = o.OfficeName;

            context.SaveChanges();

            return Ok("Modified Office name");
        }

        // PUT: api/Office/5
        /// <summary>
        /// Changes the content of the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult ChangeOfficeContent(long id, [FromQuery] OfficeEmployeeListDTO o)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");
            
            var targetOffice = context.Offices.Where(of => of.Id == id).Include(of => of.Employees).FirstOrDefault();

            targetOffice.Description = o.OfficeName;
            targetOffice.Employees = new List<Employee>();

            foreach (EmployeeDTO employee in o.Employees)
            {
                targetOffice.Employees.Add(new Employee
                {
                    FirstName = employee.FName,
                    LastName = employee.LName
                });
            }
          
            context.SaveChanges();

            return Ok("Modified Office content");
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the office and employees.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOfficeAndEmployees(long id)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            var o = context.Offices.Find(id);
            context.Offices.Remove(o);

            context.SaveChanges();

            return Ok("Deleted");
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the just employees.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteJustEmployees(long id)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");


            var o = context.Offices.Include(of => of.Employees).FirstOrDefault(of => of.Id == id);
            o.Employees.RemoveRange(0, count:o.Employees.Count);

            context.SaveChanges();

            return Ok("Deleted");
        }
    }
}
