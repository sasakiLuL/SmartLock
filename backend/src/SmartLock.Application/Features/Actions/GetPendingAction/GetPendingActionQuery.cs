using MediatR;

namespace SmartLock.Application.Features.Actions.GetPendingAction;

public record GetPendingActionQuery(Guid DeviceId) : IRequest<ActionResponse>;