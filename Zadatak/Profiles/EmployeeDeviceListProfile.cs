using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class EmployeeDeviceListProfile : Profile
    {
        public EmployeeDeviceListProfile()
        {
            CreateMap<Employee, EmployeeOfficeInfoDeviceListDTO>()
                .ForMember(dest => dest.FName, source => source.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LName, source => source.MapFrom(src => src.LastName))
                .ForMember(dest => dest.OfficeName, source => source.MapFrom(src => src.Office.Description))
                .ForMember(dest => dest.DeviceUsageList, source => source.MapFrom(src => src.Devices));
        }
    }
}
