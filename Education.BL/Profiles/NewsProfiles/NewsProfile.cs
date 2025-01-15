using AutoMapper;
using Education.BL.DTOs;
using Education.Core.Models;

namespace Education.BL.Profiles;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<NewsCreateDTO, News>().ReverseMap();
        CreateMap<NewsUpdateDTO, News>().ReverseMap();
        CreateMap<NewsListItemDTO, News>().ReverseMap();
        CreateMap<NewsViewItemDTO, News>().ReverseMap();
    }
}
