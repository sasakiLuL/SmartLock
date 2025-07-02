using MediatR;

namespace SmartLock.Application.Features.Actions.GetById;

public record GetActionByIdQuery(Guid ActionId) : IRequest<ActionResponse>;
