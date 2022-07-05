using BlazorFormsAPI.Models;
using BlazorFormsAPI.Repositories;
using BlazorFormsAPI.Utils.JWT;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BlazorFormsAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;

        public AccountController(IConfiguration configuration, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ValidateUser()
        {
            string windowsUser = HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(windowsUser))
            {
                return Unauthorized();
            }

            User user = await _accountRepository.GetUserByName(windowsUser);
            if (user is null) 
            {
                return Unauthorized();
            }
            string token = JwtHandler.GenerateJwtToken(user, _configuration["JwtKey"]);
            user.Token = token;

            return Ok(user);
        }
    }
}
