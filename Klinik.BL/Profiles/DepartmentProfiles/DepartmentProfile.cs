using AutoMapper;
using Klinik.BL.DTOs;
using Klinik.Core.Models;

namespace Klinik.BL.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentCreateDTO, Department>().ReverseMap();
        CreateMap<DepartmentUpdateDTO, Department>().ReverseMap();
        CreateMap<DepartmentListItemDTO, Department>().ReverseMap();
        CreateMap<DepartmentViewItemDTO, Department>().ReverseMap();
    }
}
