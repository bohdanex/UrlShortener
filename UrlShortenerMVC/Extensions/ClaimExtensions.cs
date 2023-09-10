using System;
using System.Linq;
using System.Security.Claims;
using UrlShortener.ObjectModel;

namespace UrlShortenerMVC.Extensions
{
    public static class ClaimExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static Role? GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                string claimRole = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
                return (Role)int.Parse(claimRole);
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }
    }
}
