using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class UsageDeviceProfile : Profile
    {
        public UsageDeviceProfile()
        {
            CreateMap<DeviceUsage, UsageDeviceDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Device.Name))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To));
            CreateMap<DeviceUsage, UsageDeviceDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Device.Name))
                .ForMember(dest => dest.From, source => source.MapFrom(src => src.From))
                .ForMember(dest => dest.To, source => source.MapFrom(src => src.To)).ReverseMap();
        }
    }
}
