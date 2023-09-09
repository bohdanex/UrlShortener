using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;
using UrlShortener.ObjectModel.DTO;
using UrlShortener.ObjectModel.UriModels;
using UrlShortener.Services.Abstraction;
using UrlShortenerMVC.Attributes;

namespace UrlShortenerMVC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : Controller
    {
        private readonly IBaseUrlService baseUrlService;

        public UrlShortenerController(IBaseUrlService baseUrlService)
        {
            this.baseUrlService = baseUrlService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Json(await baseUrlService.GetAll());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUrl(CreateUrlDTO baseUrlDTO)
        {
            var urlFromDb = await baseUrlService.GetByUrl(baseUrlDTO.URL);
            if(urlFromDb != null)
            {
                return StatusCode(400, new ErrorResponseModel("This url is already created"));
            }
            BaseUrl url = new BaseUrl();
            url.OriginalURL = baseUrlDTO.URL;
            url.UserId = User.GetId();
            await baseUrlService.Create(url);
            return StatusCode((int)HttpStatusCode.Accepted);
        }

        [HttpGet("/rd/{shortenedUrl}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string shortenedUrl)
        {
            var redirectUrl = (await baseUrlService.GetByShortenedUrl(shortenedUrl));
            if (redirectUrl == null)
            {
                return Redirect("~/");
            }
            return new RedirectResult(redirectUrl.OriginalURL);
        }
    }
}
