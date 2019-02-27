using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class OfficeEmployeeListProfile : Profile
    {
        public OfficeEmployeeListProfile()
        {
            CreateMap<Office, OfficeEmployeeListDTO>()
                .ForMember(dest => dest.OfficeName, source => source.MapFrom(src => src.Description))
                .ForMember(dest => dest.Employees, source => source.MapFrom(src => src.Employees));
        }
    }
}
