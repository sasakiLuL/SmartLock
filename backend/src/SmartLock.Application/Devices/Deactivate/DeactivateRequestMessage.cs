using SmartLock.Application.Abstractions.Messages;

namespace SmartLock.Application.Devices.Deactivate;

public record DeactivateRequestMessage(
    Guid HardwareId) : DeviceMessage(HardwareId);
