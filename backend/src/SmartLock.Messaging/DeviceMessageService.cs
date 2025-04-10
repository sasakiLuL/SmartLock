using Microsoft.Extensions.Options;
using MQTTnet;
using SmartLock.Messaging.Options;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Authentication;
using SmartLock.Application.Abstractions.Messages;
using Microsoft.Extensions.DependencyInjection;
using SmartLock.Messaging.Abstractions;
using System.Text.Json;
using SmartLock.Application.Devices.ActivationResponse;
using SmartLock.Application.Devices.Deactivate;

namespace SmartLock.Messaging;

public class DeviceMessageService : IDeviceMessageService
{
    private readonly MqttOptions _mqttOptions;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IMqttClient _client;

    public DeviceMessageService(IServiceScopeFactory scopeFactory, IOptions<MqttOptions> mqttOptions)
    {
        _mqttOptions = mqttOptions.Value ?? throw new ArgumentNullException(nameof(mqttOptions));

        _scopeFactory = scopeFactory;

        _client = new MqttClientFactory().CreateMqttClient();

        _client.ApplicationMessageReceivedAsync += async e =>
        {
            using var scope = scopeFactory.CreateScope();

            var handler = scope.ServiceProvider
                .GetServices<IDeviceMessageConsumerHandler>()
                .FirstOrDefault(
                    h => h.MessagePolicy == ConvertFromFullStringToEnum(e.ApplicationMessage.Topic));

            var messageJson = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            if (handler != null)
            {

                DeviceMessage message = handler.MessagePolicy switch
                {
                    MessagePolicy.Deactivations => JsonSerializer.Deserialize<DeactivateRequestMessage>(messageJson) as DeviceMessage,
                    MessagePolicy.ActivationResponses => JsonSerializer.Deserialize<ActivationResponseMessage>(messageJson) as DeviceMessage,
                    _ => throw new Exception("No message class was found")
                }
                ?? throw new Exception("No message class was found");

                await handler.HandleAsync(message);
            }
        };
    }

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        X509Certificate2 pemCertificate =
            X509Certificate2.CreateFromPemFile(
                _mqttOptions.CertificatePath,
                _mqttOptions.PrivateKeyPath);

#pragma warning disable SYSLIB0057 // Type or member is obsolete
        var clientCertificate = new X509Certificate2(pemCertificate.Export(X509ContentType.Pfx));

        var rootCaCert = new X509Certificate2(_mqttOptions.RootCAPath);
#pragma warning restore SYSLIB0057 // Type or member is obsolete

        var tlsOptions = new MqttClientTlsOptionsBuilder()
            .WithSslProtocols(SslProtocols.Tls12)
            .WithClientCertificates([clientCertificate, rootCaCert])
            .WithAllowUntrustedCertificates(false)
            .Build();

        var clientOptions = new MqttClientOptionsBuilder()
            .WithClientId(_mqttOptions.ClientId)
            .WithTcpServer(_mqttOptions.Endpoint, _mqttOptions.Port)
            .WithTlsOptions(tlsOptions)
            .Build();

        await _client.ConnectAsync(clientOptions, cancellationToken);
    }

    public async Task SubscribeAsync(CancellationToken cancellationToken = default)
    {
        var options = new MqttClientSubscribeOptionsBuilder();

        foreach (var policy in _mqttOptions.ConsumerPolicies)
        {
            options.WithTopicFilter($"{_mqttOptions.PoliciesBase}/+/{policy.Value}");
        }

        await _client.SubscribeAsync(options.Build(), cancellationToken);
    }

    public async Task PublishAsync(string topic, byte[] payload, CancellationToken cancellationToken = default)
    {
        var mqttMessage = new MqttApplicationMessageBuilder()
           .WithTopic(topic)
           .WithPayload(payload)
           .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
           .Build();

        await _client.PublishAsync(mqttMessage, cancellationToken);
    }

    public void Dispose()
    {
        _client?.Dispose();
    }

    private MessagePolicy ConvertFromFullStringToEnum(string fullPolicyString)
    {
        foreach (var policy in _mqttOptions.ConsumerPolicies)
        {
            if (fullPolicyString.Contains(policy.Value))
            {
                return Enum.Parse<MessagePolicy>(policy.Key);
            }
        }

        throw new ArgumentNullException(nameof(MessagePolicy));
    }
}
