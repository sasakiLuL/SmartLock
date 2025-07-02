using MediatR;

namespace SmartLock.Domain.Features.Devices;

public record DeviceActivated(
    Guid HardwareId, 
    Guid ActionId, 
    string Username,
    DateTime ActivatedOn) : INotification;
