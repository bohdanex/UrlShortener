using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess;
using UrlShortener.ObjectModel.UriModels;
using UrlShortenerDataAccess.Repositories.Abstraction;

namespace UrlShortenerDataAccess.Repositories
{


    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext appDbContext;

        public UrlRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task Create(BaseUrl baseUrl)
        {
            appDbContext.BaseURLs.Add(baseUrl);
            await appDbContext.SaveChangesAsync();
        }

        public async Task Delete(BaseUrl baseUrl)
        {
            appDbContext.Attach<BaseUrl>(baseUrl).State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var url = appDbContext.BaseURLs.FirstOrDefaultAsync();
            if (url != null)
            {
                appDbContext.Remove(url);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BaseUrl>> GetAll(int page, int pageSize)
        {
            return await appDbContext.BaseURLs
                .OrderByDescending(x => x.CreationDate)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }
        public async Task<IEnumerable<BaseUrl>> GetAll()
        {
            return await appDbContext.BaseURLs.OrderByDescending(x => x.CreationDate).ToArrayAsync();
        }
        public async Task<BaseUrl> GetByEncryptedUrl(string shortenedUrl)
        {
            return await appDbContext.BaseURLs.FirstOrDefaultAsync(x => x.ShortenedURL == shortenedUrl);
        }

        public async Task<BaseUrl> GetById(Guid id)
        {
            return await appDbContext.BaseURLs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BaseUrl> GetByOriginalUrl(string originalUrl)
        {
            return await appDbContext.BaseURLs.FirstOrDefaultAsync(x => x.OriginalURL == originalUrl);
        }
    }
}
