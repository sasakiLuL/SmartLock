using MediatR;

namespace SmartLock.Application.Actions.GetById;

public record GetActionsByIdQuery(Guid ActionId) : IRequest<ActionResponse>;
