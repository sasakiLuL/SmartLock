using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Application.Devices.ConfirmActivation;
using SmartLock.Application.Devices.Deactivate;
using System.Reflection;

namespace SmartLock.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IDeviceMessageConsumerHandler, ActivationResponseMessageConsumerHandler>();

        services.AddScoped<IDeviceMessageConsumerHandler, DeactivateRequestMessageHandler>();

        return services;
    }
}
