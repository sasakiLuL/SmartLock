namespace SmartLock.Messaging.Abstractions;

public interface IDeviceMessageService : IDisposable
{
    Task ConnectAsync(CancellationToken cancellationToken = default);

    Task SubscribeAsync(CancellationToken cancellationToken = default);

    Task PublishAsync(string topic, byte[] payload, CancellationToken cancellationToken = default);
}
