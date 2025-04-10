namespace SmartLock.Application.Abstractions.Messages;

public interface IDeviceMessageConsumerHandler
{
    MessagePolicy MessagePolicy { get; }

    Task HandleAsync(DeviceMessage message, CancellationToken cancellationToken = default);
}
