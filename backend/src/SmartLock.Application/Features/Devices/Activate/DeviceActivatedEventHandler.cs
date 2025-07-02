using MediatR;
using SmartLock.Application.Shadows;
using SmartLock.Application.Shadows.Model;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Messaging;
using SmartLock.Messaging.Producers;

namespace SmartLock.Application.Features.Devices.Activate;

public class DeviceActivatedEventHandler(IMessageProducer messageProducer) : INotificationHandler<DeviceActivated>
{
    public async Task Handle(DeviceActivated notification, CancellationToken cancellationToken)
    {
        var stateMessage = new Message<Shadow>(
            notification.HardwareId,
            new Shadow(
                State: new ShadowState(
                    Desired: new DesiredShadow(
                        Action: new ActionRequestShadowModel(
                            ActionId: notification.ActionId,
                            ActionType: (int)ActionType.Activate,
                            ActionArguments: notification.Username)),
                    Reported: null)));

        await messageProducer.ProduceAsync(stateMessage, cancellationToken);
    }
}
