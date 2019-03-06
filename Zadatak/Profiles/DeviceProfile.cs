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
            CreateMap<DeviceDTO, Device>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                .ForMember(dest => dest.Employee, source => source.Ignore())
                .ForMember(dest => dest.EmployeeId, source => source.MapFrom(src => src.Employee.EmployeeId));
            CreateMap<Device, DeviceDTO>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                .ForMember(dest => dest.Employee, source => source.MapFrom(src => src.Employee));
        }
    }
}
