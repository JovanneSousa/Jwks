using Jovanne.Jwks.Client.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.JwtExtensions;

namespace Jovanne.Jwks.Client
{
    public static class IdentityClientConfig
    {
        public static IServiceCollection AddJovanneJwksClient(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isDevelopment)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (string.IsNullOrEmpty(jwtSettings?.AutenticacaoJwksUrl))
                throw new InvalidOperationException("Url JWT não configurado.");
            var issuer = jwtSettings.Issuer ?? jwtSettings.AutenticacaoJwksUrl;

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = !isDevelopment;
                o.SaveToken = true;
                o.SetJwksOptions(new JwkOptions($"{jwtSettings.AutenticacaoJwksUrl}/jwks", issuer));

                o.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            Success = false,
                            Errors = new[]
                            {
                                "Usuário não autenticado!"
                            }
                        });
                    },

                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            Success = false,
                            Errors = new[]
                            {
                                "Você não tem permissão para acessar este recurso!"
                            }
                        });
                    }
                };
            });


            return services;
        }
    }
}
