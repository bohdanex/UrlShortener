using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;
using UrlShortenerMVC.Attributes;

namespace UrlShortenerMVC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : Controller
    {
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return View();
        }

        //[AuthorizeRoles(Role.User)]
        //public async Task<IActionResult> AddUrlHash()
        //{

        //}
    }
}
