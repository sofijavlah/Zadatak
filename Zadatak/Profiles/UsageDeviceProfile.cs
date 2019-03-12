using AutoMapper;
using Zadatak.DTOs.Usage;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class UsageDeviceProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageDeviceProfile"/> class.
        /// </summary>
        public UsageDeviceProfile()
        {
            CreateMap<DeviceUsage, UsageDeviceDto>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Device.Name))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To));
            CreateMap<DeviceUsage, UsageDeviceDto>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Device.Name))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To)).ReverseMap();
        }
    }
}
