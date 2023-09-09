using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel.UriModels;
using UrlShortener.Services.Abstraction;
using UrlShortenerDataAccess.Repositories.Abstraction;

namespace UrlShortener.Services.Implementation
{
    public class BaseUrlService : IBaseUrlService
    {
        private readonly IUrlRepository urlRepository;
        private readonly IUrlHasherService urlHasherService;

        public BaseUrlService(IUrlRepository urlRepository, IUrlHasherService urlHasherService)
        {
            this.urlRepository = urlRepository;
            this.urlHasherService = urlHasherService;
        }

        public async Task Create(BaseUrl baseUrl)
        {
            baseUrl.CreationDate = DateTime.Now;
            while (String.IsNullOrEmpty(baseUrl.ShortenedURL))
            {
                string hash = await urlHasherService.Hash(baseUrl.OriginalURL);
                var urlFromDb = await urlRepository.GetByEncryptedUrl(hash);
                if(urlFromDb == null)
                {
                    baseUrl.ShortenedURL = hash;
                }
            }

            await urlRepository.Create(baseUrl);
        }

        public async Task Delete(BaseUrl baseUrl)
        {
            await urlRepository.Delete(baseUrl);
        }

        public async Task Delete(Guid id)
        {
            await urlRepository.Delete(id);
        }

        public async Task<IEnumerable<BaseUrl>> GetAll()
        {
           return await urlRepository.GetAll();
        }

        public async Task<BaseUrl> GetById(Guid id)
        {
            return await urlRepository.GetById(id);
        }

        public async Task<BaseUrl> GetByShortenedUrl(string url)
        {
            return await urlRepository.GetByEncryptedUrl(url);
        }

        public async Task<BaseUrl> GetByUrl(string url)
        {
            return await urlRepository.GetByOriginalUrl(url);
        }
    }
}
