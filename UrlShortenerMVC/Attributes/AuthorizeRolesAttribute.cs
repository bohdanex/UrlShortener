using Microsoft.AspNetCore.Authorization;
using UrlShortener.ObjectModel;

namespace UrlShortenerMVC.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
