using SmartLock.Api.Extensions;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Messaging.Options;
using System.Reflection;

namespace SmartLock.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.Configure<MqttOptions>(configuration.GetSection(MqttOptions.Section));

        return services;
    }
}
