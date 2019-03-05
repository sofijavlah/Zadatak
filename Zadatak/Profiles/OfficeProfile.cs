using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zadatak.Models;
using Zadatak.DTOs;

namespace Zadatak.Profiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<OfficeDTO, Office>()
                .ForMember(dest => dest.Description, source => source.MapFrom(src => src.OfficeName))
                .ForMember(dest => dest.Employees, source => source.Ignore());
        }
    }
}
