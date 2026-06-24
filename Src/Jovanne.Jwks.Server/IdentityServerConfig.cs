using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt.Core.Jwa;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace JovanneIdentityServer
{
    public static class IdentityServerConfig
    {
        public static IServiceCollection AddJovanneJwksServer<TUser, TRole, TDbContext>
            (this IServiceCollection services)
        where TRole : IdentityRole
        where TUser : IdentityUser
        where TDbContext : DbContext, ISecurityKeyContext
        {
            services.AddJwksManager(options =>
                options.Jws = Algorithm.Create(DigitalSignaturesAlgorithm.RsaSsaPssSha256))
                .PersistKeysToDatabaseStore<TDbContext>();

            services.AddIdentity<TUser, TRole>()
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
