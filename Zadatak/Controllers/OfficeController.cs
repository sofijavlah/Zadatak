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
    [ApiController]
    public class OfficeController : BaseController<Office, OfficeDTO>
    {

        private readonly IMapper _mapper;
        private readonly WorkContext context;
        private readonly DbSet<Office> dbSet;

        public OfficeController(IMapper mapper, WorkContext contextt) : base(mapper, contextt)
        {
        }

        // GET: api/Office
        /// <summary>
        /// Gets the offices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        private IActionResult GetOffices()
        {
            return Ok(base.GetAll());
        }

        // GET: api/Office/5
        /// <summary>
        /// Gets the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        private IActionResult GetOffice(long id)
        {
            return Ok(base.Get(id));
        }

        // POST: api/Office 
        /// <summary>
        /// Adds the office.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        private IActionResult PostOffice(OfficeDTO o)
        {
            return Ok(base.Post(o));
        }

        // POST: api/Office 
        /// <summary>
        /// Adds the office and employees.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOfficeAndEmployees(OfficeEmployeeListDTO o)
        {
            
            var offices = base.Post(_mapper.Map(o, new Office()));
            var num = offices.Where(x => x.Description == o.OfficeName);
           if (!num.Any()) return BadRequest("Office Already Exists");

            var newOffice = new Office();
            _mapper.Map(o, newOffice);

            dbSet.Add(newOffice);
            context.SaveChanges();

            return Ok("Added Office and Employees");
        }

        // PUT: api/Office/5
        /// <summary>
        /// Changes the name of the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut]
        private IActionResult ChangeOfficeName(long id, OfficeDTO o)
        {
            return Ok(base.Put(id, o));
        }

        // PUT: api/Office/5
        /// <summary>
        /// Changes the content of the office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangeOfficeContent(long id, OfficeEmployeeListDTO o)
        {
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");
            
            var targetOffice = context.Offices.Where(of => of.Id == id).Include(of => of.Employees).FirstOrDefault();

            _mapper.Map(o, targetOffice);
            
            context.SaveChanges();

            return Ok("Modified Office content");
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes the office and employees.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        private IActionResult DeleteOffice(long id)
        {
            return Ok(base.Delete(id));
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
            if (context.Offices.Find(id) == null) return NotFound("Office doesn't exist");

            var o = context.Offices.Include(of => of.Employees).FirstOrDefault(of => of.Id == id);
            o.Employees.RemoveRange(0, count:o.Employees.Count);

            context.SaveChanges();

            return Ok("Deleted");
        }
    }
}
