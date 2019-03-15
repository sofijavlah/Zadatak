using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Zadatak.DTOs.Device;
using Zadatak.DTOs.Usage;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class DeviceController : BaseController<Device, DeviceDto>
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IDeviceRepository _repository;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IUsageRepository _usageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="usageRepository">The usage repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public DeviceController(IMapper mapper, IDeviceRepository repository, IEmployeeRepository employeeRepository, IUsageRepository usageRepository) 
            : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _employeeRepository = employeeRepository;
            _usageRepository = usageRepository;
        }
        
        // GET: api/Device
        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDeviceUseHistory(long id)
        {
            var device = _repository.GetDeviceUseHistory(id);

            if (device == null) return BadRequest("Device doesn't exist");

            var usages = device.UsageList.Select(x => _mapper.Map<UsageUserDto>(x));

            return Ok(usages);
        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDeviceCurrentInfo(long id)
        {
            var device = _repository.GetDeviceCurrentInfo(id);

            if (device == null) return BadRequest("Device doesn't exist");

            var currInfo = device.UsageList.Where(x => x.To == null).Select(x => _mapper.Map<UsageUserDto>(x));

            return Ok(currInfo);
        }

        /// <summary>
        /// Posts the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        public override IActionResult Post(DeviceDto dto)
        {
            _repository.Add(dto);

            var device = _repository.GetDeviceByName(dto.Name);

            _usageRepository.Add(device);

            return Ok("Device added");
        }

        /// <summary>
        /// Changes device name OR user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>
        /// BadRequest if device with given id doesn't exist or if employee with given id doesn't exist. Ok with message "Changed name" OR "Changed User". 
        /// </returns>
        [HttpPut]
        public override IActionResult Put(long id, DeviceDto dto)
        {
            var device = _repository.GetDeviceCurrentInfo(id);

            var usage = device.UsageList.FirstOrDefault(x => x.To == null);

            _usageRepository.Update(usage);
            _repository.ChangeDeviceNameOrUser(id, dto);
            _usageRepository.Add(device);

            return Ok("Changed");

        }

    }
}
