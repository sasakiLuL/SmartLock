using SmartLock.Domain.Entities;

namespace SmartLock.Domain.Features.Devices.States;

public class StateModel : Entity
{
    private StateModel() { }

    internal StateModel(
        Guid id,
        Guid deviceId)
    {
        Id = id;
        DeviceId = deviceId;
        Locked = false;
        Status = DeviceStatus.Unactivated;
        LastUpdatedOnUtc = DateTime.UtcNow;
    }

    public Guid DeviceId { get; internal set; }

    public DateTime LastUpdatedOnUtc { get; internal set; }

    public bool Locked { get; internal set; }

    public DeviceStatus Status { get; internal set; }
}
