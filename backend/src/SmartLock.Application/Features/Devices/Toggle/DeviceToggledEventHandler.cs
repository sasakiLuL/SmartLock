using MediatR;
using SmartLock.Application.Shadows;
using SmartLock.Application.Shadows.Model;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Messaging;
using SmartLock.Messaging.Producers;

namespace SmartLock.Application.Features.Devices.Toggle;

public class DeviceToggledEventHandler(IMessageProducer messageProducer) : INotificationHandler<DeviceToggled>
{
    public async Task Handle(DeviceToggled notification, CancellationToken cancellationToken)
    {
        var stateMessage = new Message<Shadow>(
            notification.HardwareId,
            new Shadow(
                State: new ShadowState(
                    Desired: new DesiredShadow(
                        Action: new ActionRequestShadowModel(
                            ActionId: notification.ActionId,
                            ActionType: notification.Locked ? 
                                (int)ActionType.Lock : 
                                (int)ActionType.Unlock,
                            ActionArguments: null)),
                    Reported: null)));

        await messageProducer.ProduceAsync(stateMessage, cancellationToken);
    }
}
