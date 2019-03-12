using AutoMapper;
using Zadatak.DTOs.Employee;
using Zadatak.Models;

namespace Zadatak.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeProfile"/> class.
        /// </summary>
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName, source => source.MapFrom(src => src.FName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(src => src.LName))
                .ForMember(dest => dest.OfficeId, source => source.MapFrom(src => src.Office.OfficeId))
                .ForMember(dest => dest.Office, source => source.Ignore())
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.EmployeeId));
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FName, source => source.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LName, source => source.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Office, source => source.MapFrom(src => src.Office))
                .ForMember(dest => dest.EmployeeId, source => source.MapFrom(src => src.Id));
        }
    }
}
