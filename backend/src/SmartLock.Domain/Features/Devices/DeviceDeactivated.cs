using MediatR;

namespace SmartLock.Domain.Features.Devices;

public record DeviceDeactivated(
    Guid HardwareId, 
    Guid ActionId, 
    DateTime DeactivatedOn) : INotification;