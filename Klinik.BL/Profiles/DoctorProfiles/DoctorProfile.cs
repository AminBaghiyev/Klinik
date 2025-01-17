using AutoMapper;
using Klinik.BL.DTOs;
using Klinik.Core.Models;

namespace Klinik.BL.Profiles;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<DoctorCreateDTO, Doctor>().ReverseMap();
        CreateMap<DoctorUpdateDTO, Doctor>().ReverseMap();
        CreateMap<DoctorListItemDTO, Doctor>()
            .ReverseMap()
            .ForMember(dest => dest.DepartmentTitle, options => options.MapFrom(src => src.Department.Title));
        CreateMap<DoctorViewItemDTO, Doctor>()
            .ReverseMap()
            .ForMember(dest => dest.DepartmentTitle, options => options.MapFrom(src => src.Department.Title));
    }
}
