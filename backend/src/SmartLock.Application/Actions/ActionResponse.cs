using SmartLock.Domain.Actions;

namespace SmartLock.Application.Actions;

public record ActionResponse(
    Guid Id,
    CommandType ActionType,
    DateTime OccuredOn);
