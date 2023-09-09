using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Services.Abstraction;

namespace UrlShortener.Services
{
    public class SHA1Hasher : IUrlHasherService
    {
        public async Task<string> Hash(string url)
        {
            byte[] urlAsByteArray = Encoding.ASCII.GetBytes(url);
            using (SHA1 sha1 = SHA1.Create())
            {
                using (Stream stream = new MemoryStream(urlAsByteArray))
                {
                    byte[] result = await sha1.ComputeHashAsync(stream);
                    return Convert.ToHexString(result);
                }
            }
        }
    }
}
