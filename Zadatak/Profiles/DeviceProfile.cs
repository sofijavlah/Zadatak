using AutoMapper;
using Zadatak.DTOs.Device;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DeviceProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceProfile"/> class.
        /// </summary>
        public DeviceProfile()
        {
            CreateMap<DeviceDto, Device>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                .ForMember(dest => dest.Employee, source => source.Ignore())
                .ForMember(dest => dest.EmployeeId, source => source.MapFrom(src => src.Employee.EmployeeId));
            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                .ForMember(dest => dest.Employee, source => source.MapFrom(src => src.Employee));
        }
    }
}
