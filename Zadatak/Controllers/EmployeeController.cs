using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zadatak.Attributes;
using Zadatak.DTOs.Employee;
using Zadatak.DTOs.Usage;
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
    public class EmployeeController : BaseController<Employee, EmployeeDto>
    {

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEmployeeRepository _repository;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        public EmployeeController(IMapper mapper, IEmployeeRepository repository)
            : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Employee doesn't exist</exception>
        [NoUnitOfWork]
        [HttpGet]
        public IActionResult GetEmployeeUseHistory(long id)
        {
            var employee = _repository.GetEmployeeUseHistory(id);

            if (employee == null) throw new CustomException("Employee doesn't exist");

            if (!employee.UsageList.Any()) return Ok("Employee hasn't used any devices");

            var usages = employee.UsageList.Select(x => _mapper.Map<UsageDeviceDto>(x));

            return Ok(usages);
        }

        
    }
}
