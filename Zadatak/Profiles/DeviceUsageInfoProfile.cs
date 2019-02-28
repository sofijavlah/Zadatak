using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class DeviceUsageInfoProfile : Profile
    {
        public DeviceUsageInfoProfile()
        {
            CreateMap<DeviceUsage, DeviceUsageInfoDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Device.Name))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To));
        }
    }
}
