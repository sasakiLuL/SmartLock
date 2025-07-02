using SmartLock.Domain.Entities;

namespace SmartLock.Domain.Features.Devices.Actions;

public class ActionModel : Entity
{
    private ActionModel() { }

    internal ActionModel(
        Guid id,
        Guid userId,
        Guid deviceId,
        ActionType type)
    {
        Id = id;
        UserId = userId;
        DeviceId = deviceId;
        Type = type;
        Status = ActionStatus.Pending;
        RequestedOn = DateTime.UtcNow;
        ExecutedOn = null;
    }

    public Guid UserId { get; internal set; }

    public ActionType Type { get; internal set; }

    public ActionStatus Status { get; internal set; }

    public DateTime RequestedOn { get; internal set; }

    public DateTime? ExecutedOn { get; internal set; }

    public Guid DeviceId { get; internal set; }
}
