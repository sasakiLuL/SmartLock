namespace SmartLock.Messaging.Producers;

public interface IMessageProducer
{
    Task ProduceAsync<TPayload>(Message<TPayload> message, CancellationToken cancellationToken = default)
        where TPayload : class;
}