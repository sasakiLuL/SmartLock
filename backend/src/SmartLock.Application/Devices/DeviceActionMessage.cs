using SmartLock.Application.Abstractions.Messages;
using SmartLock.Domain.Actions;

namespace SmartLock.Application.Devices;

public record DeviceActionMessage(
    Guid HardwareId,
    CommandType CommandType) : DeviceMessage(HardwareId);
