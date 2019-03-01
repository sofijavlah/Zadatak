using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class EmployeeDeviceUsageListProfile : Profile
    {
        public EmployeeDeviceUsageListProfile()
        {
            CreateMap<Employee, EmployeeDeviceUsageListDTO>()
                .ForMember(dest => dest.FName, source => source.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LName, source => source.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Usages, source => source.MapFrom(src => src.UsageList));
            CreateMap<Employee, EmployeeDeviceUsageListDTO>()
                .ForMember(dest => dest.FName, source => source.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LName, source => source.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Usages, source => source.MapFrom(src => src.UsageList)).ReverseMap();
        }
    }
}
