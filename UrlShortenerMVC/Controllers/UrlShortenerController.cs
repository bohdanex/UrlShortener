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
using UrlShortenerMVC.Extensions;

namespace UrlShortenerMVC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : Controller
    {
        const int PAGE_SIZE = 10;
        const string REDIRECT_SYMBOL = "r";
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
        /// <param name="page">Current page</param>
        [AllowAnonymous]
        [HttpGet]
        [Route("get-all/{page:int?}")]
        public async Task<IActionResult> GetAll(int? page)
        {
            var allUrls = await baseUrlService.GetAll(page.GetValueOrDefault(), PAGE_SIZE);
            IEnumerable<SimpleUrlDTO> allUrlDTOs = mapper.Map<IEnumerable<BaseUrl>, IEnumerable<SimpleUrlDTO>>(allUrls);
            return Json(allUrlDTOs);
        }

        /// <summary>
        /// Gets a url by its id
        /// </summary>
        [HttpGet]
        [Route("get/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var url = await baseUrlService.GetById(id);
            if(url == null)
            {
                return StatusCode(400, new ClientErrorResponse("Not found"));
            }
            url.User.BaseURLs = null;
            return Json(url);
        }

        /// <summary>
        /// Creates a new shortened url
        /// </summary>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUrl(CreateUrlDTO baseUrlDTO)
        {
            baseUrlDTO.URL = baseUrlDTO.URL.Trim();
            var urlFromDb = await baseUrlService.GetByUrl(baseUrlDTO.URL);
            if(urlFromDb != null)
            {
                return StatusCode(400, new ClientErrorResponse("This url is already created"));
            }
            BaseUrl url = new BaseUrl();
            url.OriginalURL = baseUrlDTO.URL;
            url.UserId = User.GetId();
            var createdUrl = await baseUrlService.Create(url, HttpContext.Request.RedirectURL(REDIRECT_SYMBOL));
            SimpleUrlDTO simpleCreatedUrl = mapper.Map<BaseUrl, SimpleUrlDTO>(createdUrl);
            return Json(simpleCreatedUrl);
        }

        /// <summary>
        /// Redirect to full url
        /// </summary>
        /// <param name="shortenedUrl">Short url created by URLShortener service</param>
        [HttpGet("/" + REDIRECT_SYMBOL + "/{shortenedUrl}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string shortenedUrl)
        {
            var redirectUrl = (await baseUrlService.GetByShortenedUrl(HttpContext.Request.RedirectURL(REDIRECT_SYMBOL) + shortenedUrl));
            if (redirectUrl == null)
            {
                return Redirect("~/");
            }
            return new RedirectResult(redirectUrl.OriginalURL);
        }


        /// <summary>
        /// Deletes a url by its id
        /// </summary>
        /// <param name="id">Shortened url id</param>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            BaseUrl url = await baseUrlService.GetById(id);
            if(url == null)
            {
                return StatusCode(400, new ClientErrorResponse("URL not found"));
            }
            if(User.GetRole().Value == Role.User && url.User.Id != User.GetId())
            {
                return StatusCode(400, new ClientErrorResponse("No rights for this action"));
            }
            await baseUrlService.Delete(url);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpGet("/about")]
        [AllowAnonymous]
        public IActionResult About()
        {
            AboutDTO aboutDTO = new AboutDTO
            {
                AdditionalText = string.Empty,
                IsAdmin = User.GetRole() != null && User.GetRole().Value == Role.Admin
            };
            return View(aboutDTO);
        }
    }
}
