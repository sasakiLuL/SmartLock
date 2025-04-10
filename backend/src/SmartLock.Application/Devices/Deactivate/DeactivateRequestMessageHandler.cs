using SmartLock.Application.Abstractions;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Domain.Devices;

namespace SmartLock.Application.Devices.Deactivate;

public class DeactivateRequestMessageHandler(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IDeviceMessageConsumerHandler
{
    public MessagePolicy MessagePolicy => MessagePolicy.Deactivations;

    public async Task HandleAsync(DeviceMessage message, CancellationToken cancellationToken = default)
    {
        var conMessage = message as DeactivateRequestMessage;

        var device = await deviceRepository.ReadByHardwareIdAsync(conMessage!.HardwareId, cancellationToken);

        if (device is null)
        {
            return;
        }

        deviceRepository.Delete(device);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
