using Api.JwT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using Transversal.Extensions;
using Transversal.Helpers.JWT;

namespace Api.Configurations
{
    public static class JwtStartup
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // método de extensión propio para registrar settings al DI y obtener el objeto instanciado
            var jwtBearerTokenSettings = services.AddConfig<IJwtBearerTokenSettings, JwtBearerTokenSettings>
                (configuration, sectionKey: nameof(JwtBearerTokenSettings));

            var refreshTokenSection = services.AddConfig<IRefreshTokenSettings, RefreshTokenSettings>
                (configuration, sectionKey: nameof(RefreshTokenSettings));

            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateIssuer = true,
                ValidIssuer = jwtBearerTokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtBearerTokenSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments(configuration["HubPath"])))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<IJwtBearerTokenHelper>(new JwtBearerTokenHelper(jwtBearerTokenSettings, tokenValidationParameters));
            services.AddSingleton<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
