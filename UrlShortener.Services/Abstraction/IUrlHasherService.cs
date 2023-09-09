using System.Threading.Tasks;

namespace UrlShortener.Services.Abstraction
{
    public interface IUrlHasherService
    {
        Task<string> Hash(string url);
    }
}