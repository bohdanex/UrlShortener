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
using AutoMapper;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UrlShortenerMVC.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace UrlShortenerMVC.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;

        public AccountController(IUserService userService, ISecurityService securityService, IMapper mapper)
        {
            this.userService = userService;
            this.securityService = securityService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserAuthModel userAuthModel)
        {
            var actionResult = ValidateRegistration(userAuthModel);
            if(actionResult != null)
            {
                return actionResult;
            }

            User createdUser = await CreateUserWithHashedPassword(userAuthModel);
            createdUser.Role = Role.User;
            string accessToken = GenerateAccessToken(createdUser);
            
            return Json(new AuthResponse(accessToken));            
        }

        #region private members for Register
        private async Task<User> CreateUserWithHashedPassword(UserAuthModel userAuthModel)
        {
            User user = mapper.Map<UserAuthModel, User>(userAuthModel);
            user.Salt = securityService.GenerateSalt();
            user.SaltedHashedPassword = securityService.GetSaltedHashedPassword(userAuthModel.Password, user.Salt);
            return await userService.Create(user);
        }
        private IActionResult ValidateRegistration(UserAuthModel userAuthModel)
        {
            bool isValidEmail = MailAddress.TryCreate(userAuthModel.Email, out _);
            if (!isValidEmail)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ClientErrorResponse("Not valid email"));
            }
            if (!IsValidPassword(userAuthModel.Password))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ClientErrorResponse("Not valid password"));
            }

            return null;
        }
        private bool IsValidPassword(string password)
        {
            return !password.Contains(" ") && password.Length >= 8;
        }
        #endregion

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
                return StatusCode((int)HttpStatusCode.Unauthorized, new ClientErrorResponse("User not found"));
            }

            string salt = user.Salt;
            if (!securityService.CheckPasswordIdentity(userAuthModel.Password, salt, user.SaltedHashedPassword))
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new ClientErrorResponse("User not found"));
            }
            
            return Json(new AuthResponse(GenerateAccessToken(user)));
        }

        private static string GenerateAccessToken(User user)
        {
            var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()), new(ClaimTypes.Role, user.Role.ToString("d")) };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [Route("register-admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(UserAuthModel userAuthModel)
        {
            var actionResult = ValidateRegistration(userAuthModel);
            if (actionResult != null)
            {
                return actionResult;
            }

            User createdUser = await CreateUserWithHashedPassword(userAuthModel);
            createdUser.Role = Role.Admin;
            string accessToken = GenerateAccessToken(createdUser);

            return Json(new AuthResponse(accessToken));
        }

        /// <summary>
        /// Call this endpoint to receive basic information about a user
        /// Authorize http header required
        /// </summary>
        [HttpPost]
        [Route("get-user-info")]
        [AuthorizeRoles(Role.Admin, Role.User)]
        public async Task<IActionResult> GetUserInfo()
        {
            Guid id = User.GetId();
            User user = await userService.GetById(id);
            if(user == null)
            {
                return StatusCode(500, new ClientErrorResponse("User not found"));
            }
            UserDTO userDTO = mapper.Map<User, UserDTO>(user);
            return Json(userDTO);
        }
    }
}
