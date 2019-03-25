using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Zadatak.Attributes;
using Zadatak.DTOs.Device;
using Zadatak.DTOs.Usage;
using Zadatak.Exceptions;
using Zadatak.Expressions;
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
        
        private readonly IUsageRepository _usageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="usageRepository">The usage repository.</param>
        public DeviceController(IMapper mapper, IDeviceRepository repository, IUsageRepository usageRepository) 
            : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _usageRepository = usageRepository;
        }

        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Device doesn't exist</exception>
        [NoUnitOfWork]
        // GET: api/Device
        [HttpPost]
        public IActionResult GetDeviceUseHistory(long id)
        {
            var device = _repository.GetDeviceUseHistory(id);

            if (device == null) throw new CustomException("Device doesn't exist");

            var usages = device.UsageList.Select(x => _mapper.Map<UsageUserDto>(x));

            return Ok(usages);
        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">Device doesn't exist</exception>
        [NoUnitOfWork]
        [HttpPost]
        public IActionResult GetDeviceCurrentInfo(long id)
        {
            var device = _repository.GetDeviceCurrentInfo(id);

            if (device == null) throw new CustomException("Device doesn't exist");

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

        /// <summary>
        /// Gets the query smor.
        /// </summary>
        /// <param name="queryInfo">The query information.</param>
        /// <returns></returns>
        [NoUnitOfWork]
        [HttpPost]
        public IActionResult GetQuerySmor(QueryInfo queryInfo)
        {
            var usages = _usageRepository.GetAll().AsQueryable().ToList();

            var result = queryInfo.GetSorted(queryInfo, usages).ProjectTo<UsageDto>(_mapper.ConfigurationProvider);

            return Ok(result);
        }
    }
}
