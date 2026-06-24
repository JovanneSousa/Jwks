using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Jovanne.Jwks.Client.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

        public IEnumerable<Claim> GetClaimsIdentity() =>
            _accessor.HttpContext?.User?.Claims ?? new List<Claim>();

        public string GetUserEmail() =>
            IsAuthenticated() ? _accessor.HttpContext?.User.GetUserEmail() ?? string.Empty : string.Empty;

        public string GetUserId() =>
            IsAuthenticated() ? _accessor.HttpContext?.User.GetUserId() ?? string.Empty : string.Empty;

        public bool IsAuthenticated() =>
            _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string role) =>
            _accessor.HttpContext?.User.IsInRole(role) ?? false;
    }
}
