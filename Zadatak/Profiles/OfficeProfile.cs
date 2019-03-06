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
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.OfficeId));
            CreateMap<Office, OfficeDTO>()
                .ForMember(dest => dest.OfficeName, source => source.MapFrom(src => src.Description))
                .ForMember(dest => dest.OfficeId, source => source.MapFrom(src => src.Id));
        }
    }
}
