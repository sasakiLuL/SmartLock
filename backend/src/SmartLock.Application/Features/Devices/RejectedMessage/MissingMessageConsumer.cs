using SmartLock.Messaging.Consumers;
using SmartLock.Messaging;
using SmartLock.Domain.Features.Devices;
using SmartLock.Application.Interfaces;
using SmartLock.Application.Shadows;

namespace SmartLock.Application.Features.Devices.RejectedMessage;

public class MissingMessageConsumer(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IMessageConsumer<ShadowErrorMessage>
{
    public async Task ConsumeAsync(Message<ShadowErrorMessage> message, CancellationToken cancellationToken = default)
    {
        switch (message.Payload.Code)
        {
            case 404:
                {
                    var device = await deviceRepository.ReadByHardwareIdAsync(message.HardwareId, cancellationToken);

                    if (device is null)
                    {
                        return;
                    }

                    device.HandleDeviceMissing();

                    await unitOfWork.CommitAsync(cancellationToken);
                }
                break;
            default:
                {
                    return;
                }
        }
    }
}
