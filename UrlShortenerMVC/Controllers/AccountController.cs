using Microsoft.AspNetCore.Mvc;
using UrlShortener.ObjectModel.DTO;

namespace UrlShortenerMVC.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Authorizes a user based on email and password
        /// </summary>
        /// <returns>Access token</returns>
        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserAuthModel userAuthModel)
        {
            string accessToken = string.Empty;
            return Json(accessToken);
        }
    }
}
