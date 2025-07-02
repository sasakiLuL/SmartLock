using SmartLock.Domain.Features.Devices.Actions;

namespace SmartLock.Application.Features.Actions;

public record ActionResponse(
    Guid Id,
    Guid DeviceId,
    Guid UserId,
    ActionType Type,
    ActionStatus Status,
    DateTime RequestedOn,
    DateTime? ExecutedOn);
