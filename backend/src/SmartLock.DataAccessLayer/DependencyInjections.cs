using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartLock.Application.Interfaces;
using SmartLock.DataAccessLayer.Features.Devices;
using SmartLock.DataAccessLayer.Features.Users;
using SmartLock.DataAccessLayer.Interceptors;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;

namespace SmartLock.DataAccessLayer;

public static class DependencyInjections
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string is not exist");

        services.AddScoped<PublishDomainEventsInterceptor>();

        services.AddDbContext<SmartLockContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>();
            options
                .UseNpgsql(
                    connectionString,
                    options => options.EnableRetryOnFailure())
                .AddInterceptors([interceptor]);
        });

        services.AddScoped<IReadModelService, ReadModelService>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IDeviceRepository, DeviceRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
