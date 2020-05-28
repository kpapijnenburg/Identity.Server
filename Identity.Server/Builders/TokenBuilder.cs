using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Server.Builders
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IConfiguration _config;
        private List<Claim> _claims = new List<Claim>();
        public TokenBuilder(IConfiguration config)
        {
            _config = config;
        }
        public string Build()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               _config["Jwt:Issuer"],
               _config["Jwt:Audience"],
               _claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ITokenBuilder Create()
        {
            return new TokenBuilder(_config);
        }

        public ITokenBuilder WithClaim(Claim claim)
        {
            _claims.Add(claim);
            return this;
        }
    }
}
