using SmartLock.Domain.Core;
using SmartLock.Domain.Shared.DeviceName;

namespace SmartLock.Domain.Devices;

public class Device : Entity<DeviceModel>
{
    private Device(DeviceModel model) : base(model) {}

    public Guid Id { get => _model.Id; }

    public Guid HardwareId { get => _model.HardwareId; }

    public DeviceName? DeviceName 
    {
        get => _model.DeviceName is not null ? 
            new DeviceName(_model.DeviceName) : 
            null;

        set => _model.DeviceName = value?.Value ?? null;
    }

    public DeviceStatus DeviceStatus 
    {
        get => _model.DeviceStatus;
        set => _model.DeviceStatus = value;
    }

    public DateTime RegisteredOnUtc 
    {
        get => _model.RegisteredOnUtc;
        set => _model.RegisteredOnUtc = value;
    }

    public Guid OwnerId 
    { 
        get => _model.OwnerId; 
        set => _model.OwnerId = value;
    }

    public static Device Create(
        Guid id,
        Guid hardwareId,
        DeviceName? deviceName,
        DeviceStatus deviceStatus,
        DateTime registeredOnUtc,
        Guid ownerId)
    {
        var model = new DeviceModel()
        {
            Id = id,
            HardwareId = hardwareId,
            DeviceName = deviceName?.Value ?? null,
            DeviceStatus = deviceStatus,
            RegisteredOnUtc = registeredOnUtc,
            OwnerId = ownerId
        };

        return new Device(model);
    }
}
