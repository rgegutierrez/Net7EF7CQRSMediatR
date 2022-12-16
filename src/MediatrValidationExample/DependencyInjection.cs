using FluentValidation.AspNetCore;
using MediatrExample.ApplicationCore.Common.Interfaces;
using MediatrExample.WebApi.Filters;
using MediatrExample.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace MediatrExample.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation().AddJsonOptions(x => 
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}