using MQTTnet;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Authentication;
using SmartLock.Messaging.Consumers;
using MQTTnet.Protocol;

namespace SmartLock.Messaging;

public class MessagingService
{
    private readonly IMqttClient _client;

    private readonly ConsumerDispatcher _dispatcher;

    private readonly ConnectionOptions _options;

    public MessagingService(
        ConnectionOptions options,
        List<string> topicsToSubscribe,
        ConsumerDispatcher consumerDispatcher)
    {
        _options = options;

        _dispatcher = consumerDispatcher;

        _client = new MqttClientFactory().CreateMqttClient();

        _client.ApplicationMessageReceivedAsync += async e =>
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            await _dispatcher.DispatchAsync(topic, payload);
        };

        _client.ConnectedAsync += async e =>
        {
            foreach (var policy in topicsToSubscribe)
            {
                await _client.SubscribeAsync(new MqttTopicFilterBuilder()
                    .WithTopic(policy)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build());
            }
        };
    }

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        X509Certificate2 pemCertificate =
            X509Certificate2.CreateFromPemFile(
                _options.CertificatePath,
                _options.PrivateKeyPath);

#pragma warning disable SYSLIB0057 // Type or member is obsolete
        var clientCertificate = new X509Certificate2(pemCertificate.Export(X509ContentType.Pfx));

        var rootCaCert = new X509Certificate2(_options.RootCAPath);
#pragma warning restore SYSLIB0057 // Type or member is obsolete

        var tlsOptions = new MqttClientTlsOptionsBuilder()
            .WithSslProtocols(SslProtocols.Tls12)
            .WithClientCertificates([clientCertificate, rootCaCert])
            .WithAllowUntrustedCertificates(false)
            .Build();

        var clientOptions = new MqttClientOptionsBuilder()
            .WithClientId(_options.ClientId)
            .WithTcpServer(_options.Host, _options.Port)
            .WithTlsOptions(tlsOptions)
            .Build();

        await _client.ConnectAsync(clientOptions, cancellationToken);
    }

    public async Task PublishAsync(string topic, byte[] payload, CancellationToken cancellationToken = default)
    {
        var mqttMessage = new MqttApplicationMessageBuilder()
           .WithTopic(topic)
           .WithPayload(payload)
           .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
           .Build();

        await _client.PublishAsync(mqttMessage, cancellationToken);
    }
}
