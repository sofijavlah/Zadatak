using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>()
                .ForMember(dest => dest.FirstName, source => source.MapFrom(src => src.FName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(src => src.LName))
                .ForMember(dest => dest.OfficeId, source => source.MapFrom(src => src.Office.OfficeId))
                .ForMember(dest => dest.Office, source => source.Ignore())
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.EmployeeId));
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.FName, source => source.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LName, source => source.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Office, source => source.MapFrom(src => src.Office))
                .ForMember(dest => dest.EmployeeId, source => source.MapFrom(src => src.Id));
        }
    }
}
