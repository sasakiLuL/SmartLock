using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLock.Application.Features.Devices.Activate;
using SmartLock.Application.Features.Devices.Deactivate;
using SmartLock.Application.Features.Devices.RejectedMessage;
using SmartLock.Application.Features.Devices.StateReceived;
using SmartLock.Application.Features.Devices.Toggle;
using SmartLock.Application.Options;
using SmartLock.Application.Shadows;
using SmartLock.Messaging.Extensions;
using System.Reflection;

namespace SmartLock.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        MqttOptions mqttOptions = configuration.GetSection(MqttOptions.Section).Get<MqttOptions>()
            ?? throw new InvalidDataException();

        services.AddMessaging(conf =>
        {
            conf.Host((options) =>
            {
                options.Host = mqttOptions.Endpoint;
                options.Port = mqttOptions.Port;
                options.ClientId = mqttOptions.ClientId;
                options.CertificatePath = mqttOptions.CertificatePath;
                options.RootCAPath = mqttOptions.RootCAPath;
                options.PrivateKeyPath = mqttOptions.PrivateKeyPath;
            });

            conf.ConsumerTopic<MissingMessageConsumer, ShadowErrorMessage>(mqttOptions.ConsumerPolicies["UpdateRejected"]);
            conf.ConsumerTopic<StateReceivedMessageConsumer, Shadow>(mqttOptions.ConsumerPolicies["UpdateAccepted"]);

            conf.MapProducerTopic<Shadow>(mqttOptions.ProducerPolicies["Update"]);
        });

        return services;
    }
}
