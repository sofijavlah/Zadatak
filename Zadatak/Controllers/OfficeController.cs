using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Clauses;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {

        private readonly WorkContext context;

        public OfficeController(WorkContext _context)
        {
            context = _context;
            context.Offices.Include(office => office.Employees).ThenInclude(employee => employee.Devices);
            context.SaveChanges();
        }

        // GET: api/Office
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
        [HttpGet("{id}")]
        public IActionResult GetOffice(long id)
        {
            if (context.Offices.Include(o => o.Employees).FirstOrDefault(o => o.Id == id) == null)
            {
                return NotFound("Office doesn't exist");
            }

            var targetOffice = context.Offices.Include(o => o.Employees).Where(o => o.Id == id).Select(l => new OfficeEmployeeListDTO
            {
                OfficeName = l.Description,
                EmployeeList = l.Employees.Select(e => new EmployeeDTO
                {
                    FName = e.FirstName,
                    LName = e.LastName
                })
            });
            
            return Ok(targetOffice);
        }

        // POST: api/Office 
        [HttpPost]
        public IActionResult AddOffice(OfficeDTO o)
        {
            if(context.Offices.Count(of => of.Description == o.OfficeName) == 1) return BadRequest("Office Already Exists");
            
            Office office = new Office();
            office.Description = o.OfficeName;

            context.Offices.Add(office);
            context.SaveChanges();

            return Ok("Added");
        }

        // POST: api/Office 
        [HttpPost]
        public IActionResult AddOfficeAndEmployees(OfficeEmployeeListDTO o)
        {
            if (context.Offices.Count(of => of.Description == o.OfficeName) > 0) return BadRequest("Office Already Exists");

            Office office = new Office();
            office.Description = o.OfficeName;

            office.Employees = new List<Employee>();
            foreach (EmployeeDTO employee in o.EmployeeList)
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
        [HttpPut("{id}")]
        public IActionResult ChangeOfficeName(long id, OfficeDTO o)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            var targetOffice = context.Offices.Where(of => of.Id == id).Include(of => of.Employees).FirstOrDefault();

            targetOffice.Description = o.OfficeName;

            context.SaveChanges();

            return Ok("Modified Office name");
        }

        // PUT: api/Office/5
        [HttpPut("{id}")]
        public IActionResult ChangeOfficeContent(long id, OfficeEmployeeListDTO o)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");
            
            var targetOffice = context.Offices.Where(of => of.Id == id).Include(of => of.Employees).FirstOrDefault();

            targetOffice.Description = o.OfficeName;
            targetOffice.Employees = new List<Employee>();

            foreach (EmployeeDTO employee in o.EmployeeList)
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
