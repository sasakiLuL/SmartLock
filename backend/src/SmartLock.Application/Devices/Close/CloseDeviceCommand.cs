using MediatR;

namespace SmartLock.Application.Devices.Close;

public record CloseDeviceCommand(
    Guid DeviceId) : IRequest;
