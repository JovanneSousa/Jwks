using System.Security.Claims;

namespace Jovanne.Jkws.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal));

            var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value ?? string.Empty;
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal));

            var claim = claimsPrincipal.FindFirst(ClaimTypes.Email);
            return claim?.Value ?? string.Empty;
        }
    }
}
