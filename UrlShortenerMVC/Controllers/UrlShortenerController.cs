using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UrlShortenerMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> AddUrlHash()
        //{

        //}
    }
}
