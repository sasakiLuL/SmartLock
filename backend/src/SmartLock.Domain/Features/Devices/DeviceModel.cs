using SmartLock.Domain.Entities;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Domain.ValueObjects.DeviceNames;

namespace SmartLock.Domain.Features.Devices;

public class DeviceModel : AggregateRoot
{
    internal readonly List<ActionModel> _actions = [];

    private DeviceModel() { }

    internal DeviceModel(
        Guid id,
        Guid ownerId,
        Guid hardwareId,
        DeviceName deviceName,
        StateModel stateModel)
    {
        Id = id;
        OwnerId = ownerId;
        HardwareId = hardwareId;
        DeviceName = deviceName;
        RegisteredOnUtc = DateTime.UtcNow;
        State = stateModel;
    }

    public DeviceName DeviceName { get; internal set; } = null!;

    public DateTime RegisteredOnUtc { get; internal set; }

    public Guid HardwareId { get; internal set; }

    public Guid OwnerId { get; internal set; }

    public StateModel State { get; internal set; } = null!;

    public IReadOnlyCollection<ActionModel> Actions => _actions.AsReadOnly();
}
