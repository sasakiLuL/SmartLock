using SmartLock.Application.Abstractions.Messages;

namespace SmartLock.Messaging.Abstractions;

public interface IDeviceMessageConsumer
{
    Task StartConsumingAsync(CancellationToken cancellationToken = default);
}
