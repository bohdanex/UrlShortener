using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel.UriModels;

namespace UrlShortener.Services.Abstraction
{
    public interface IBaseUrlService
    {
        Task<IEnumerable<BaseUrl>> GetAll(int page, int pageSize);
        Task<IEnumerable<BaseUrl>> GetAll();
        Task<BaseUrl> GetById(Guid id);
        Task<BaseUrl> GetByUrl(string url);
        Task<BaseUrl> GetByShortenedUrl(string url);
        Task<BaseUrl> Create(BaseUrl baseUrl, string domain);
        Task Delete(BaseUrl baseUrl); 
        Task Delete(Guid id); 
    }
}
