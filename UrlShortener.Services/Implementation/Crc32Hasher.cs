using Force.Crc32;
using System;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Services.Abstraction;

namespace UrlShortener.Services.Implementation
{
    public class Crc32Hasher : IUrlHasherService
    {
        public async Task<string> Hash(string url)
        {
            return await Task.Run(() =>
            {
                var crc32 = new Crc32Algorithm();
                byte[] urlAsByteArray = Encoding.ASCII.GetBytes(url);
                byte[] hash = crc32.ComputeHash(urlAsByteArray);
                return BitConverter.ToUInt32(hash, 0).ToString("X");
            });
        }
    }
}
