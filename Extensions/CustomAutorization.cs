using Microsoft.AspNetCore.Http;

namespace Jovanne.Jkws.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimUsuario(HttpContext? context, string claimName, string claimValue)
        {
            if (context is null) return false; 

            return (context.User.Identity?.IsAuthenticated ?? false) && 
                context.User.Claims.Any(c => c.Type.Equals(claimName) && c.Value.Equals(claimValue));
        }
    }
}
