using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartLock.Application.Abstractions;
using SmartLock.Authorization.IdentityProvider;
using SmartLock.Authorization.Options;

namespace SmartLock.Authorization;

public static class DependencyInjections 
{
    public static IServiceCollection AddKeycloack(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationOptions = configuration
            .GetSection(AuthenticationOptions.Section)
            .Get<AuthenticationOptions>() ?? throw new ArgumentException(nameof(AuthenticationOptions));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MetadataAddress = $"{authenticationOptions.Address}/realms/{authenticationOptions.Realm}/.well-known/openid-configuration";
                options.RequireHttpsMetadata = authenticationOptions.RequireHttpsMetadata ?? true;

                options.Audience = authenticationOptions.Audience;

                options.IncludeErrorDetails = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = authenticationOptions.ValidateIssuer ?? true,
                    ValidIssuer = $"{authenticationOptions.Address}/realms/{authenticationOptions.Realm}",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddAuthorization();

        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.Section));

        services.AddHttpContextAccessor();

        services.AddHttpClient<IIdentityService, IdentityService>();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IUserCredentialsProvider, UserCredentialsProvider>();

        return services;
    }
}
