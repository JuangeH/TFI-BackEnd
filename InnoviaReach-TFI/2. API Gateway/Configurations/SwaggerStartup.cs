using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Api.Configurations
{
    public static class SwaggerStartup
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("InnoviaReachAPISpecification", new OpenApiInfo { Title = "InnoviaReach API", Version = "1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition(securityScheme.Scheme, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        securityScheme, new string[] { "Bearer" }
                    }
                });

                c.CustomSchemaIds(type => type.ToString());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
