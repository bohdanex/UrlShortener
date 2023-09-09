using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Services.Abstraction
{
    public interface ISecurityService
    {
        string GetSaltedHashedPassword(string password, string salt);
        bool CheckPasswordIdentity(string password, string salt, string saltedHashedPassword);
        string GenerateSalt();
    }
}
