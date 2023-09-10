using System;
using System.Linq;
using System.Security.Claims;
using UrlShortener.ObjectModel;

namespace UrlShortenerMVC
{
    public static class ClaimsExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static Role GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            string claimRole = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            return (Role)Int32.Parse(claimRole);
        }
    }
}
