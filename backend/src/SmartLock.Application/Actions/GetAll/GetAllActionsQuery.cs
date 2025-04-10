using MediatR;

namespace SmartLock.Application.Actions.GetAll;

public record GetAllActionsQuery : IRequest<List<ActionResponse>>;
