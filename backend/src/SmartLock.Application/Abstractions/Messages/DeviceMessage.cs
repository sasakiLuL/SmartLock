namespace SmartLock.Application.Abstractions.Messages;

public abstract record DeviceMessage(
    Guid HardwareId);
