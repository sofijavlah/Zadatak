using AutoMapper;
using Zadatak.Models;
using Zadatak.DTOs.Office;

namespace Zadatak.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class OfficeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeProfile"/> class.
        /// </summary>
        public OfficeProfile()
        {
            CreateMap<OfficeDto, Office>()
                .ForMember(dest => dest.Description, source => source.MapFrom(src => src.OfficeName))
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.OfficeId));
            CreateMap<Office, OfficeDto>()
                .ForMember(dest => dest.OfficeName, source => source.MapFrom(src => src.Description))
                .ForMember(dest => dest.OfficeId, source => source.MapFrom(src => src.Id));
        }
    }
}
