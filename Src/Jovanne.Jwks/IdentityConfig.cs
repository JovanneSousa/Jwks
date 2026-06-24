using Jovanne.Jwks.Client;
using JovanneIdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace Jovanne.Jwks
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddJovanneJwksFull<TUser, TRole, TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isDevelopment
        )
            where TRole : IdentityRole
            where TUser : IdentityUser
            where TDbContext : DbContext, ISecurityKeyContext
        {
            return services
                .AddJovanneJwksServer<TUser, TRole, TDbContext>()
                .AddJovanneJwksClient(configuration, isDevelopment);
        }
    }
}
