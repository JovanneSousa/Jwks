using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetDevPack.Security.JwtExtensions;

namespace Jovanne.Jwks.Core
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityClientConfig(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (string.IsNullOrEmpty(jwtSettings?.AutenticacaoJwksUrl))
                throw new InvalidOperationException("Url JWT não configurado.");
            var issuer = jwtSettings.Issuer ?? jwtSettings.AutenticacaoJwksUrl;

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
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


            return builder;
        }
    }
}
