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
                var user = await _userManager.FindByNameAsync(credentials.Username);

                string token = _tokenBuilder.Create()
                    .WithClaim(new Claim("id", user.Id))
                    .WithClaim(new Claim("username", user.UserName))
                    .Build();


                var output = new LoginOutputModel() {Id = user.Id,  Username = user.UserName, Token = token };

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