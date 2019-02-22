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
        [HttpGet("OfficeList")]
        public IActionResult GetOffices()
        {
            var offices = context.Offices.Select(o => new OfficeDTO(o));
            return Ok(offices);
        }

        // GET: api/Office/5
        [HttpGet("{id}")]
        public IActionResult GetOffice(long id)
        {
            if (context.Offices.Include(o => o.Employees).First(o => o.Id == id) == null)
            {
                return NotFound();
            }

            var targetOffice = context.Offices.Include(o => o.Employees).Where(o => o.Id == id).Select(l => new OfficeEmployeeListDTO
            {
                OfficeName = l.Description,
                EmployeeList = l.Employees.Select(e => new EmployeeDTO(e))
            });
            
            return Ok(targetOffice);
        }

        // POST: api/Office 
        [HttpPost]
        public IActionResult Post(OfficeDTO o)
        {
            
            Office office = new Office();
            office.Description = o.OfficeName;

            context.Offices.Add(office);
            context.SaveChanges();

            return Ok("Added");

        }

        // PUT: api/Office/5
        [HttpPut("ChangeName/{id}")]
        public IActionResult ChangeNameOfOffice(int id, OfficeDTO o)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            //var office = context.Offices.Where(of => of.Id == id).Include(of => of.Employees);
            //var targetOffice = office.First();

            var targetOffice = context.Offices.Where(of => of.Id == id).Include(of => of.Employees).Select(of => of.Description);

            Office office = new Office();
            office.Description = o.OfficeName;

            context.SaveChanges();

            return Ok("Modified");
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            var o = context.Offices.Find(id);
            context.Offices.Remove(o);

            context.SaveChanges();

            return Ok("Deleted");
        }
    }
}
