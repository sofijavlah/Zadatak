using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zadatak.Attributes;
using Zadatak.DTOs.Employee;
using Zadatak.DTOs.Office;
using Zadatak.Exceptions;
using Zadatak.Interfaces;
using Zadatak.Models;

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

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public OfficeController(IMapper mapper, IOfficeRepository repository) : base (mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [NoUnitOfWork]
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
            var office = _repository.GetOffice(description);

            if (office == null) throw new CustomException("Office doesn't exist");

            if (!office.Employees.Any()) return Ok("Office doesn't have any employees");

            var list = office.Employees.Select(x => _mapper.Map<EmployeeDto>(x));

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
            _unitOfWork.Start(_unitOfWork.GetReadOnly());

            var office = _repository.Get(id);

            if (office == null) throw new CustomException("Office doesn't exist");

            if (!office.Employees.Any()) return Ok("No employees in this office");

            office.Employees.RemoveRange(0, office.Employees.Count);
            
            return Ok("Employees removed");
        }
    }
}
