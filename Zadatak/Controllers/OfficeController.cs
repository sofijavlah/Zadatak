using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    [ApiController]
    public class OfficeController : BaseController<Office, OfficeDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="context">The context.</param>
        public OfficeController(IMapper mapper, WorkContext context) : base(mapper, context)
        {
        }

        // GET: api/Office
        /// <summary>
        /// Gets the offices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOffices()
        {
            return base.GetAll();
        }

        // GET: api/Office/5
        /// <summary>
        /// Gets the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOffice(long id)
        {
            return base.Get(id);
        }

        // GET: api/Office/dto
        /// <summary>
        /// Gets the office employees.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetOfficeEmployees(OfficeDTO o)
        {
            var office = Context.Offices.Include(x => x.Employees).FirstOrDefault(x => x.Description == o.OfficeName);
            if (office == null) return BadRequest("Office doesn't exist");

            var employees = office.Employees.Select(x => Mapper.Map(x, new EmployeeDTO()));

            return Ok(employees);
        }

        // POST: api/Office 
        /// <summary>
        /// Adds the office.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostOffice(OfficeDTO o)
        {
            return base.Post(o);
        }

        // PUT: api/Office/5
        /// <summary>
        /// Changes the name of the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeOfficeName(long id, OfficeDTO o)
        {
            return base.Put(id, o);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the office and employees.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteOffice(long id)
        {
            return base.Delete(id);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the just employees.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteJustEmployees(long id)
        {
            if (Context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            var o = Context.Offices.Include(x => x.Employees).FirstOrDefault(x => x.Id == id);

            o.Employees.RemoveRange(0, o.Employees.Count);

            Context.SaveChanges();

            return Ok("Deleted");
        }
    }
}
