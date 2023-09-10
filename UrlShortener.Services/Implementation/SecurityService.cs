using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;
using UrlShortener.Services.Abstraction;

namespace UrlShortener.Services.Implementation
{
    public class SecurityService : ISecurityService
    {

        public bool CheckPasswordIdentity(string password, string salt, string saltedHashedPassword)
        {
            return saltedHashedPassword == GetSaltedHashedPassword(password, salt);
        }

        public string GetSaltedHashedPassword(string password, string salt)
        {
            var sha256 = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }

        public string GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
