using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartLock.Messaging.Consumers;

public class ConsumerDispatcher(IServiceScopeFactory scopeFactory, TopicConsumerRegistry registry)
{
    public async Task DispatchAsync(string topic, string jsonPayload, CancellationToken cancellationToken = default)
    {
        if (registry.ResolveConsumer(topic, out var hardwareId) is not { } descriptor)
        {
            return;
        }

        var payload = JsonSerializer.Deserialize(
            jsonPayload,
            descriptor.PayloadType, 
            new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            })
            ?? throw new Exception("Payload deserialization failed.");

        var messageType = typeof(Message<>).MakeGenericType(descriptor.PayloadType);
        var messageInstance = Activator.CreateInstance(messageType, hardwareId, payload);

        using var scope = scopeFactory.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var consumer = scopedProvider.GetRequiredService(descriptor.HandlerType);

        var method = descriptor.HandlerType.GetMethod("ConsumeAsync", new[] { messageType, typeof(CancellationToken) })
            ?? throw new InvalidOperationException("ConsumeAsync method not found on consumer.");

        await (Task)method.Invoke(consumer, [messageInstance!, cancellationToken])!;
    }
}
