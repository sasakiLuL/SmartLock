namespace SmartLock.Application.Abstractions.Messages;

public interface IDeviceMessagePublisher
{
    Task PublishAsync(DeviceMessage message, MessagePolicy policy, CancellationToken cancellationToken = default);
}