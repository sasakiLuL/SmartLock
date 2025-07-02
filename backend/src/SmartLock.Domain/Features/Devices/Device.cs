using SmartLock.Domain.Entities;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Domain.ValueObjects.DeviceNames;
using System;

namespace SmartLock.Domain.Features.Devices;

public class Device(DeviceModel model) : IDomainEntity<DeviceModel>
{
    public DeviceModel Model => model;

    private void CancelAllPendingActions()
    {
        var pendingActions = model._actions
            .Where(x => x.Status == ActionStatus.Pending);

        foreach (var pendingAction in pendingActions)
        {
            pendingAction.Status = ActionStatus.Cancelled;
            pendingAction.ExecutedOn = DateTime.UtcNow;
        }
    }

    private void FailAllPendingActions()
    {
        var pendingActions = model._actions
            .Where(x => x.Status == ActionStatus.Pending);
        foreach (var pendingAction in pendingActions)
        {
            pendingAction.Status = ActionStatus.Failed;
            pendingAction.ExecutedOn = DateTime.UtcNow;
        }
    }

    public void Activate(string username)
    {
        if (model.State.Status == DeviceStatus.Activated)
        {
            throw new BadRequestException(DeviceErrors.Activated(model.HardwareId));
        }

        var action = new ActionModel(Guid.NewGuid(), model.OwnerId, model.Id, ActionType.Activate);

        CancelAllPendingActions();

        model._actions.Add(action);

        model.RaiseDomainEvent(new DeviceActivated(model.HardwareId, action.Id, username, DateTime.UtcNow));
    }

    public void Lock(bool value)
    {
        if (model.State.Status == DeviceStatus.Unactivated)
        {
            throw new BadRequestException(DeviceErrors.Unactivated(model.HardwareId));
        }

        var action = new ActionModel(Guid.NewGuid(), model.OwnerId, model.Id, value ? ActionType.Lock : ActionType.Unlock);

        CancelAllPendingActions();

        model._actions.Add(action);

        model.RaiseDomainEvent(new DeviceToggled(model.HardwareId, action.Id, value, DateTime.UtcNow));
    }

    public void Deactivate()
    {
        if (model.State.Status == DeviceStatus.Unactivated)
        {
            throw new BadRequestException(DeviceErrors.Unactivated(model.HardwareId));
        }

        var action = new ActionModel(Guid.NewGuid(), model.OwnerId, model.Id, ActionType.Deactivate);

        CancelAllPendingActions();

        model._actions.Add(action);

        model.RaiseDomainEvent(new DeviceDeactivated(model.HardwareId, action.Id, DateTime.UtcNow));
    }

    public void HandleDeviceMissing()
    {
        model.State.Status = DeviceStatus.Unactivated;

        FailAllPendingActions();
    }

    public void SetState(DeviceStatus? status, bool? locked)
    {
        if (status is null && locked is null)
        {
            return;
        }

        if (status is not null)
        {
            model.State.Status = status.Value;
        }

        if (locked is not null)
        {
            model.State.Locked = locked.Value;
        }

        model.State.LastUpdatedOnUtc = DateTime.UtcNow;
    }

    public void ResolveActionStatus(Guid actionid, ActionStatus actionStatus)
    {
        var action = model._actions.FirstOrDefault(x => x.Id == actionid)
            ?? throw new NotFoundException(ActionErrors.NotFound(actionid));

        if (action.Status != ActionStatus.Pending)
        {
            throw new BadRequestException(ActionErrors.ActionAlreadyExecuted(actionid));
        }

        action.Status = actionStatus;

        action.ExecutedOn = DateTime.UtcNow;
    }

    public static Device Create(
        Guid id,
        DeviceName deviceName,
        Guid hardwareId,
        Guid ownerId)
    {
        var stateModel = new StateModel(Guid.NewGuid(), id);

        var model = new DeviceModel(id, ownerId, hardwareId, deviceName, stateModel);

        return new Device(model);
    }
}
