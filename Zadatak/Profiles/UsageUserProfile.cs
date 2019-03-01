using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class UsageUserProfile : Profile
    {
        public UsageUserProfile()
        {
            CreateMap<DeviceUsage, UsageUserDTO>()
                .ForMember(dest => dest.UserFN, source => source.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.UserLN, source => source.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To));
            CreateMap<DeviceUsage, UsageUserDTO>()
                .ForMember(dest => dest.UserFN, source => source.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.UserLN, source => source.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To)).ReverseMap();
        }
    }
}
