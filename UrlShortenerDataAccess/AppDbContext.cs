using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener.ObjectModel;
using UrlShortener.ObjectModel.UriModels;

namespace UrlShortener.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<BaseUrl> BaseURLs{ get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            builder.Entity<BaseUrl>().ToTable("ResourceLocators");
            builder.Entity<BaseUrl>().HasIndex(x => x.OriginalURL).IsUnique();
            builder.Entity<BaseUrl>().HasIndex(x => x.ShortenedURL).IsUnique();
        }
    }
}