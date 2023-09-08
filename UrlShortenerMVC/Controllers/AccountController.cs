using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using UrlShortener.ObjectModel.DTO;
using UrlShortener.ObjectModel;
using UrlShortener.Services.Abstraction;
using System.Threading.Tasks;
using System.Net;

namespace UrlShortenerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly ISecurityService securityService;

        public AccountController(IUserService userService, ISecurityService securityService)
        {
            this.userService = userService;
            this.securityService = securityService;

        }
        /// <summary>
        /// Authorizes a user based on email and password
        /// </summary>
        /// <returns>Access token</returns>
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserAuthModel userAuthModel)
        {
            User user = await userService.GetByEmail(userAuthModel.Email);
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new ErrorResponseModel("User not found"));
            }

            string salt = user.Salt;
            if (!securityService.CheckPasswordIdentity(userAuthModel.Password, salt, user.SaltedHashedPassword))
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new ErrorResponseModel("User not found"));
            }

            string accessToken = string.Empty;
            return StatusCode(401);
        }

        
    }
}
