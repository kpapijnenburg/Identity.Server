using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Server.Builders
{
    public class TokenBuilder : ITokenBuilder
    {
        public string Build()
        {
            throw new NotImplementedException();
        }

        public ITokenBuilder Create()
        {
            throw new NotImplementedException();
        }

        public ITokenBuilder WithClaim(Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}
