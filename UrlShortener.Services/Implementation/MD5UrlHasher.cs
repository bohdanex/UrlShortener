using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UrlShortener.Services.Abstraction;

namespace UrlShortener.Services
{
    public class MD5UrlHasher : IUrlHasherService
    {
        public async Task<string> Hash(string url)
        {
            return await Task.Factory.StartNew(() =>
            {
                byte[] urlAsByteArray = System.Text.Encoding.ASCII.GetBytes(url);
                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(urlAsByteArray);
                return Convert.ToBase64String(hash);
            });
        }
    }
}
