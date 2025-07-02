using MediatR;

namespace SmartLock.Application.Features.Users.Register;

public record RegisterUserCommand(
    string Email,
    string UserName) : IRequest<Guid>;
