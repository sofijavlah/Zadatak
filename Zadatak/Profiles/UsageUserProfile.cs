using AutoMapper;
using Zadatak.DTOs.Usage;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class UsageUserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageUserProfile"/> class.
        /// </summary>
        public UsageUserProfile()
        {
            CreateMap<DeviceUsage, UsageUserDto>()
                .ForMember(dest => dest.UserFn, source => source.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.UserLn, source => source.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To));
            CreateMap<DeviceUsage, UsageUserDto>()
                .ForMember(dest => dest.UserFn, source => source.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.UserLn, source => source.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To)).ReverseMap();
        }
    }
}
