using MediatR;

namespace SmartLock.Domain.Features.Devices;

public record DeviceToggled(
    Guid HardwareId, 
    Guid ActionId, 
    bool Locked, 
    DateTime ToggledOn) : INotification;