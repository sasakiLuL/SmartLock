using SmartLock.Application.Abstractions.Messages;

namespace SmartLock.Application.Devices.ActivationResponse;

public record ActivationResponseMessage(
    Guid HardwareId,
    Activation ActivationResponse) : DeviceMessage(HardwareId);