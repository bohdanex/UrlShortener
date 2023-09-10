using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel.UriModels;

namespace UrlShortenerDataAccess.Repositories.Abstraction
{
    public interface IUrlRepository
    {
        Task<IEnumerable<BaseUrl>> GetAll(int page, int pageSize);
        Task<IEnumerable<BaseUrl>> GetAll();
        Task<BaseUrl> GetById(Guid id);
        Task<BaseUrl> GetByOriginalUrl(string originalUrl);
        Task<BaseUrl> GetByEncryptedUrl(string shortenedUrl);
        Task Create(BaseUrl baseUrl);
        Task Delete(BaseUrl baseUrl);
        Task Delete(Guid id);
    }
}
