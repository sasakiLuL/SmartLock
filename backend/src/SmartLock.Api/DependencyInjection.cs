using SmartLock.Api.Extensions;
using System.Reflection;

namespace SmartLock.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        return services;
    }
}
