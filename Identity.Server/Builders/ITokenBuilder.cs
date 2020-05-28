using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Server.Builders
{
    public interface ITokenBuilder
    {
        string Build();
        ITokenBuilder Create();
        ITokenBuilder WithClaim(Claim claim);
    }
}
