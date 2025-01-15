using AutoMapper;
using Education.BL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Education.BL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegisterDTO, IdentityRole>().ReverseMap();
    }
}
