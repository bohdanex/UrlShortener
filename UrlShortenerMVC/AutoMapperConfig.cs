using AutoMapper;
using UrlShortener.ObjectModel;
using UrlShortener.ObjectModel.DTO;

namespace UrlShortenerMVC
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserAuthModel>().ReverseMap();
        }
    }
}
