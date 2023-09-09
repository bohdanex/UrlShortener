using AutoMapper;
using UrlShortener.ObjectModel;
using UrlShortener.ObjectModel.DTO;
using UrlShortener.ObjectModel.UriModels;

namespace UrlShortenerMVC
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserAuthModel>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<BaseUrl, UrlDTO>().ReverseMap();
        }
    }
}
