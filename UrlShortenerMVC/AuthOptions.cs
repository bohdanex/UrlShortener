using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UrlShortenerMVC
{
    public static class AuthOptions
    {
        public const string ISSUER = "localhost";
        public const string AUDIENCE = "localhost2";
        const string KEY = "INFORCE <3";
        public static SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
