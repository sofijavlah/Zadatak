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
            CreateMap<Office, OfficeDTO>().ForMember(dest => dest.OfficeName, source => source.MapFrom(src => src.Description));
        }
    }
}
