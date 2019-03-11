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
    public class EmployeeController : BaseController<Employee, EmployeeDTO>
    {

        private IMapper _mapper;

        private IEmployeeRepository _repository;

        private IUnitOfWork _unitOFWork;


        public EmployeeController(IMapper mapper, EmployeeRepository repository, IUnitOfWork unitOFWork) : base(mapper, repository, unitOFWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOFWork = unitOFWork;
        }

        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetEmployeeUseHistory(EmployeeDTO e)
        {
            var usages = _repository.GetEmployeeUseHistory(e.EmployeeId);

            if (!usages.Any()) return Ok("Employee hasn't used any devices");

            var history = usages.Select(x =>_mapper.Map(x, new UsageDeviceDTO()));

            return Ok(history);
        }

    }
}
