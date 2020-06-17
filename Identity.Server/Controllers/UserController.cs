using Identity.Domain;
using Identity.Server.Builders;
using Identity.Server.Extensions;
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

            if (result.Succeeded)
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier).SetType("id");
                var username = User.FindFirst(ClaimTypes.Name).SetType("username");

                string token = _tokenBuilder.Create()
                    .WithClaim(id)
                    .WithClaim(username)
                    .Build();

                var output = new LoginOutputModel() { Username = credentials.Username, Token = token };

                return Ok(output);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel credentials)
        {
            var user = new IdentityUser() { UserName = credentials.Username };

            var result = await _userManager.CreateAsync(user, credentials.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Authorized");
        }
    }
}