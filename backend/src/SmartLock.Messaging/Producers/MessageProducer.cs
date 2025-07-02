using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartLock.Messaging.Producers;

public class MessageProducer(
    MessagingService deviceMessageService,
    TopicProducerRegistry registry) : IMessageProducer
{
    public async Task ProduceAsync<TPayload>(Message<TPayload> message, CancellationToken cancellationToken = default)
        where TPayload : class
    {
        var topic = registry.GetTopicFor<TPayload>(message.HardwareId);
        var payloadJson = JsonSerializer.Serialize(
            message.Payload, 
            new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

        await deviceMessageService.PublishAsync(topic, Encoding.UTF8.GetBytes(payloadJson), cancellationToken);
    }
}
