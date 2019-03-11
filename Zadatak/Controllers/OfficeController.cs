using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Clauses;
using Zadatak.DTOs;
using Zadatak.Interfaces;
using Zadatak.Models;
using Zadatak.Repositories;

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

        private readonly IOfficeRepository _repository;

        private readonly IUnitOfWork _unitOFWork;

        public OfficeController(IMapper mapper, OfficeRepository repository, IUnitOfWork unitOFWork) : base (mapper, repository, unitOFWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOFWork = unitOFWork;
        }

        // GET: api/Office/dto
        /// <summary>
        /// Gets the office employees.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetOfficeEmployees(string description)
        {
            var office = _repository.GetOfficeEmployees(description);

            return Ok(Mapper.Map(office, new OfficeEmployeeListDTO()));
        }
        
        //[HttpDelete]
        //public IActionResult DeleteJustEmployees(string description)
        //{
        //    try
        //    {
        //        bool result = Repository.DeleteJustEmployees(description);
        //        if (result) return Ok("Deleted Employees");
        //    }

        //    catch (DbUpdateException e)
        //    {
        //        if (e.GetBaseException() is SqlException sqlException)
        //        {
        //            var exNum = sqlException.Number;
        //            if (exNum == 547) return BadRequest("Employees cannot be deleted");
        //        }
        //    }


            
        //}
    }
}
