using AutoMapper;
using Zadatak.DTOs.Usage;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class UsageProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageProfile"/> class.
        /// </summary>
        public UsageProfile()
        {
            CreateMap<DeviceUsage, UsageDto>()
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To));
            CreateMap<DeviceUsage, UsageDto>()
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To)).ReverseMap();
        }
    }
}
