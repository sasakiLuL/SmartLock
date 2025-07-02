using MediatR;
using SmartLock.Messaging.Producers;
using SmartLock.Messaging;
using SmartLock.Domain.Features.Devices;
using SmartLock.Application.Shadows;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Application.Shadows.Model;
using SmartLock.Domain.Features.Devices.Actions;

namespace SmartLock.Application.Features.Devices.Deactivate;

public class DeviceDeactivatedEventHandler(IMessageProducer messageProducer) : INotificationHandler<DeviceDeactivated>
{
    public async Task Handle(DeviceDeactivated notification, CancellationToken cancellationToken)
    {
        var stateMessage = new Message<Shadow>(
            notification.HardwareId,
            new Shadow(
                State: new ShadowState(
                    Desired: new DesiredShadow(
                        Action: new ActionRequestShadowModel(
                            ActionId: notification.ActionId,
                            ActionType: (int)ActionType.Deactivate,
                            ActionArguments: null)),
                    Reported: null)));

        await messageProducer.ProduceAsync(stateMessage, cancellationToken);
    }
}