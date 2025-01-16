using AutoMapper;
using Klinik.BL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Klinik.BL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserLoginDTO, IdentityRole>().ReverseMap();
        CreateMap<UserRegisterDTO, IdentityRole>().ReverseMap();
    }
}
