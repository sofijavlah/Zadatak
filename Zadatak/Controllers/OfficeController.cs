﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zadatak.DTOs.Employee;
using Zadatak.DTOs.Office;
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
    public class OfficeController : BaseController<Office, OfficeDto>
    {
        private readonly IMapper _mapper;

        private readonly IOfficeRepository _repository;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public OfficeController(IMapper mapper, IOfficeRepository repository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) : base (mapper, repository, unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the office employees.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>
        /// 400 error code if office doesn't exist, 200 with message: "Office doesn't have any..." if office empty or List of employees in office.
        /// </returns>
        [HttpPost]
        public IActionResult GetOfficeEmployees(string description)
        {
            var employees = _employeeRepository.GetOffice(description);

           if (!employees.Any()) return NotFound("Office doesn't have any employees");

            var list = employees.Select(x => _mapper.Map<EmployeeDto>(x));

            return Ok(list);

        }

        /// <summary>
        /// Deletes employees from office with given id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// 400 error code if office with given id doesn't exist, 200(Ok) code with appropriate message:
        /// "No employees..." if office is empty or "Employees removed" if delete was successful.
        /// </returns>
        [HttpDelete]
        public IActionResult DeleteJustEmployees(long id)
        {
            _unitOfWork.Start();

            var office = _repository.Get(id);

            if (office == null) return BadRequest("Office doesn't exist");

            if (!office.Employees.Any()) return Ok("No employees in this office");

            office.Employees.RemoveRange(0, office.Employees.Count);

            _unitOfWork.Save();
            _unitOfWork.Commit();

            return Ok("Employees removed");
        }
    }
}
