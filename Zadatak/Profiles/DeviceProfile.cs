using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name));
        }
    }
}
