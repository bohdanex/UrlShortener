using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
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
        const int PAGE_SIZE = 10;
        private readonly IBaseUrlService baseUrlService;
        private readonly IMapper mapper;

        public UrlShortenerController(IBaseUrlService baseUrlService, IMapper mapper)
        {
            this.baseUrlService = baseUrlService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Gets all urls with step of page size (currently 10)
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("get-all/{page:int?}")]
        public async Task<IActionResult> GetAll(int? page)
        {
            var allUrls = await baseUrlService.GetAll(page.GetValueOrDefault(), PAGE_SIZE);
            IEnumerable<SimpleUrlDTO> allUrlDTOs = mapper.Map<IEnumerable<BaseUrl>, IEnumerable<SimpleUrlDTO>>(allUrls);
            return Json(allUrlDTOs);
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

        [HttpGet("/r/{shortenedUrl}")]
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
