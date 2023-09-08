using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Services.Abstraction;

namespace UrlShortener.Services
{
    public class ASCIIUrlHasher : IUrlHasher
    {
        public Task<string> Hash(string url)
        {
            throw new NotImplementedException();
        }
    }
}
