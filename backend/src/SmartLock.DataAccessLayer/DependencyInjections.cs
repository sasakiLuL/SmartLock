using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLock.Application.Abstractions;
using SmartLock.DataAccessLayer.Actions;
using SmartLock.DataAccessLayer.Locks;
using SmartLock.DataAccessLayer.Users;
using SmartLock.Domain.Actions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.DataAccessLayer;

public static class DependencyInjections
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException("Connection string is not exist");

        services.AddDbContext<SmartLockContext>(options => 
            options.UseNpgsql(
                connectionString, 
                options => options.EnableRetryOnFailure()));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IDeviceRepository, DeviceRepository>();

        services.AddScoped<IActionRepository, ActionRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
