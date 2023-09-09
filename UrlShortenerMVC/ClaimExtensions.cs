using System;
using System.Linq;
using System.Security.Claims;

namespace UrlShortenerMVC
{
    public static class ClaimExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
