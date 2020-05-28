using Identity.Domain;
using Identity.Server.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenBuilder _tokenBuilder;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenBuilder tokenBuilder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenBuilder = tokenBuilder;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

            if(result.Succeeded)
            {
                string token = _tokenBuilder.Create()
                    .WithClaim(User.FindFirst(ClaimTypes.NameIdentifier))
                    .WithClaim(User.FindFirst(ClaimTypes.Name))
                    .Build();

                return Ok(token);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel credentials)
        {
            var user = new IdentityUser() { UserName = credentials.Username, Email = credentials.Email };

            var result = await _userManager.CreateAsync(user, credentials.Password);

            if(result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}