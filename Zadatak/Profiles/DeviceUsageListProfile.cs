using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class DeviceUsageListProfile : Profile
    {
        public DeviceUsageListProfile()
        {
            CreateMap<Device, DeviceUsageListDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                .ForMember(dest => dest.Usages, source => source.MapFrom(src => src.UsageList));
        }
    }
}
