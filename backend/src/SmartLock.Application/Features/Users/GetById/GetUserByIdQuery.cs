using MediatR;

namespace SmartLock.Application.Features.Users.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;
