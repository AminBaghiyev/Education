using AutoMapper;
using Education.BL.DTOs;
using Education.Core.Models;

namespace Education.BL.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryCreateDTO, Category>().ReverseMap();
        CreateMap<CategoryUpdateDTO, Category>().ReverseMap();
        CreateMap<CategoryListItemDTO, Category>().ReverseMap();
        CreateMap<CategoryViewItemDTO, Category>().ReverseMap();
    }
}
