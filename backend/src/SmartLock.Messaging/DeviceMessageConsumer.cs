using Microsoft.Extensions.Options;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Messaging.Abstractions;
using SmartLock.Messaging.Options;

namespace SmartLock.Messaging;

public class DeviceMessageConsumer(
    IDeviceMessageService deviceMessageService) : IDeviceMessageConsumer
{
    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        await deviceMessageService.SubscribeAsync(cancellationToken);
    }
}
