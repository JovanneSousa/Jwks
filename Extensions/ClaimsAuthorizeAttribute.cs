using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jovanne.Jkws.Extensions
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(
            string claimValue,
            string claimName = "permission"
            ) : base (typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
