using Microsoft.AspNetCore.Authorization;
using UrlShortener.ObjectModel;
using System.Linq;
namespace UrlShortenerMVC.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles) : base()
        {
            Roles = string.Join(",", roles.Select(x => x.ToString("d")));
        }
    }
}
