using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly WorkContext context;

        public EmployeeController(WorkContext _context)
        {
            context = _context;
            context.Employees.Include(employee => employee.Office).Include(employee => employee.Devices);
            context.SaveChanges();

        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            var employees = context.Employees.Include(em => em.Devices).Select(em => new EmployeeDTO(em));

            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (context.Employees.Include(e => e.Office).First(x => x.Id == id) == null) return NotFound();

            var targetEmployee = context.Employees.Find(id);

            var employee = new EmployeeToOfficeDTO(targetEmployee);

            return Ok(employee);

        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Post(EmployeeToOfficeDTO e)
        {
            var broj = context.Offices.Count(o => o.Description == e.OfficeName);

            if (broj == 0)
            {
                Office o = new Office();
                o.Description = e.OfficeName;

                context.Offices.Add(o);

                context.SaveChanges();

                Employee em = new Employee();
                em.FirstName = e.FName;
                em.LastName = e.LName;
                em.OfficeId = o.Id;
                context.Employees.Add(em);
                context.SaveChanges();

                return Ok("Added Employee and Office");
            }

            var office = context.Offices.First(o => o.Description == e.OfficeName);

            Employee employee = new Employee();
            employee.FirstName = e.FName;
            employee.LastName = e.LName;
            employee.OfficeId = office.Id;

            context.Employees.Add(employee);
            context.SaveChanges();

            return Ok("Added Employee");
        }
        
        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Employee e)
        {
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
