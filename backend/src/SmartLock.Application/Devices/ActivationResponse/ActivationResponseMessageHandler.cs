using SmartLock.Application.Abstractions;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Application.Devices.ActivationResponse;
using SmartLock.Domain.Devices;

namespace SmartLock.Application.Devices.ConfirmActivation;

public class ActivationResponseMessageConsumerHandler(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IDeviceMessageConsumerHandler
{
    public MessagePolicy MessagePolicy => MessagePolicy.ActivationResponses;

    public async Task HandleAsync(DeviceMessage message, CancellationToken cancellationToken = default)
    {
        var conMessage = message as ActivationResponseMessage;

        var device = await deviceRepository.ReadByHardwareIdAsync(conMessage!.HardwareId, cancellationToken);

        if (device is null)
        {
            return;
        }

        switch (conMessage.ActivationResponse)
        {
            case Activation.Accepted:
                device.DeviceStatus = DeviceStatus.Activated;
                break;

            case Activation.Rejected:
                device.DeviceStatus = DeviceStatus.Unactivated;
                break;
        }

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
