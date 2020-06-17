using System.Security.Claims;

namespace Identity.Server.Extensions
{
    public static class ClaimExtensions
    {
        public static Claim SetType(this Claim claim, string rename)
        {
            if(claim == null)
            {
                return null;
            }

            return new Claim(rename, claim.Value, claim.Type, claim.Issuer, claim.OriginalIssuer, claim.Subject);
        }
    }
}
