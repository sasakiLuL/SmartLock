namespace SmartLock.Messaging.Consumers;

public interface IMessageConsumer<TPayload> where TPayload : class
{
    Task ConsumeAsync(Message<TPayload> message, CancellationToken cancellationToken = default);
}
