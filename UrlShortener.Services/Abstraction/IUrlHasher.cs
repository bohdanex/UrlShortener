using System.Threading.Tasks;

namespace UrlShortener.Services.Abstraction
{
    public interface IUrlHasher
    {
        Task<string> Hash(string url);
    }
}