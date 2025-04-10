using MediatR;

namespace SmartLock.Application.Users.GetById;

public record GetUserByIdQuery(
    Guid Id) : IRequest<UserResponse>;
