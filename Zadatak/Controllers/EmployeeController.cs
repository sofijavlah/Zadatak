using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zadatak.DTOs.Employee;
using Zadatak.DTOs.Usage;
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

        private readonly IDeviceRepository _deviceRepository;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public EmployeeController(IMapper mapper, IEmployeeRepository repository, IDeviceRepository deviceRepository, IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the employee use history.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetEmployeeUseHistory(EmployeeDto e)
        {
            if (!_repository.GetEmployeeUseHistory(e.EmployeeId).Any()) return Ok("Employee hasn't used any devices");

            var history = _mapper.Map<UsageDeviceDto>(_repository.GetEmployeeUseHistory(e.EmployeeId));

            return Ok(history);
        }

    }
}
