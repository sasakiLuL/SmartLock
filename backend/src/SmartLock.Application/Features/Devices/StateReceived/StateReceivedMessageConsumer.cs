using SmartLock.Application.Interfaces;
using SmartLock.Application.Shadows;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Messaging;
using SmartLock.Messaging.Consumers;

namespace SmartLock.Application.Features.Devices.StateReceived;

public class StateReceivedMessageConsumer(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IMessageConsumer<Shadow>
{
    public async Task ConsumeAsync(Message<Shadow> message, CancellationToken cancellationToken = default)
    {
        var state = message.Payload.State.Reported?.State;
        
        if (state is null)
        {
            return;
        }

        var device = await deviceRepository.ReadByHardwareIdAsync(message.HardwareId, cancellationToken);

        if (device is null)
        {
            return;
        }

        device.SetState((DeviceStatus?)state.Status, state.Locked);

        var action = message.Payload.State.Reported?.Action;

        if (action is not null)
        {
            try
            {
                device.ResolveActionStatus(action.LastExecutedActionId, (ActionStatus)action.LastExecutedActionStatus);
            }
            catch (Exception)
            {
                await unitOfWork.CommitAsync(cancellationToken);

                return;
            }
        }

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
