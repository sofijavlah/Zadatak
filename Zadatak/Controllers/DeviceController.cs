using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zadatak.DTOs;
using Zadatak.DTOs.Device;
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

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public DeviceController(IMapper mapper, IDeviceRepository repository, IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        // GET: api/Device
        /// <summary>
        /// Gets the device use history.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDeviceUseHistory(DeviceDto d)
        {
            var usages = _repository.GetDeviceUseHistory(d.Id);

            var history = usages.Select(x => _mapper.Map(x, new UsageUserDto()));

            return Ok(history);
        }

        /// <summary>
        /// Gets the device current information.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDeviceCurrentInfo(DeviceDto d)
        {
            //var targetDevice = _repository.Include(x => x.Employee).Include(x => x.UsageList)
            //    .FirstOrDefault(x => x.Name == d.Name);

            //if (targetDevice == null) return NotFound("Device doesn't exist");

            //var currentUsage = targetDevice.UsageList.Where(x => x.To == null)
            //    .Select(x => Mapper.Map(x, new UsageUserDTO()));

            //return Ok(currentUsage);
            return Ok();
        }
        

        //// POST: api/Device
        ///// <summary>
        ///// Adds the device.
        ///// </summary>
        ///// <param name="d">The d.</param>
        ///// <returns>
        ///// Appropriate message if device was added, if it already exists or if employee wasn't found.
        ///// </returns>
        //[HttpPost]
        //public IActionResult PostDevice(DeviceDTO d)
        //{
        //    var device = HostingApplication.Context.Devices.Include(x => x.Employee).FirstOrDefault(x => x.Name == d.Name);

        //    if (device != null) return BadRequest("Device already exists");

        //    var newUsage = new DeviceUsage
        //    {
        //        From = DateTime.Now
        //    };

        //    base.Post(d);

        //    var newDevice = HostingApplication.Context.Devices.Include(x => x.Employee).First(x => x.Name == d.Name);

        //    var employee = newDevice.Employee;

        //    HostingApplication.Context.DeviceUsages.Add(newUsage);
        //    HostingApplication.Context.SaveChanges();
        //    newUsage.Employee = employee;
        //    newUsage.Device = newDevice;
        //    HostingApplication.Context.SaveChanges();
        //    return Ok("Added");
        //}

       
        //[HttpPut]
        //public IActionResult ChangeDeviceNameOrUser(long id, DeviceDTO d)
        //{
        //    var device = HostingApplication.Context.Devices.Include(x => x.Employee).Include(x => x.UsageList).ThenInclude(x => x.Employee)
        //        .FirstOrDefault(x => x.Id == id);

        //    if (device == null) return BadRequest("Device doesn't exist");

        //    var oldUser = device.Employee;

        //    var newUser = HostingApplication.Context.Employees.Find(d.Employee.EmployeeId);

        //    if (oldUser.Id != newUser.Id)
        //    {
        //        var oldUsage = device.UsageList.First(x => x.To == null);
        //        oldUsage.To = DateTime.Now;

        //        var newUsage = new DeviceUsage
        //        {
        //            From = DateTime.Now,

        //        };
        //        HostingApplication.Context.DeviceUsages.Add(newUsage);
        //        HostingApplication.Context.SaveChanges();
        //        newUsage.Employee = newUser;
        //        newUsage.Device = device;
        //    }

        //    return base.Put(id, d);
        //}

    }
}
