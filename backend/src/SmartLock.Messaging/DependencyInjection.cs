using Microsoft.Extensions.DependencyInjection;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Messaging.Abstractions;

namespace SmartLock.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IDeviceMessageService, DeviceMessageService>();

        services.AddSingleton<IDeviceMessageConsumer, DeviceMessageConsumer>();

        services.AddScoped<IDeviceMessagePublisher, DeviceMessagePublisher>();

        return services;
    }
}
