using SmartLock.Application.Abstractions.Messages;

namespace SmartLock.Application.Devices.ActivationRequest;

public record ActivationRequestMessage(
    Guid HardwareId,
    string Username) : DeviceMessage(HardwareId);
