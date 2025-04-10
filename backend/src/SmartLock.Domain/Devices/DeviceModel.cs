using SmartLock.Domain.Core;

namespace SmartLock.Domain.Devices;

public class DeviceModel : Model
{
    internal DeviceModel() { }

    public Guid HardwareId { get; set; }

    public string? DeviceName { get; set; }

    public DeviceStatus DeviceStatus { get; set; }

    public DateTime RegisteredOnUtc { get; set; }

    public Guid OwnerId { get; set; }
}
