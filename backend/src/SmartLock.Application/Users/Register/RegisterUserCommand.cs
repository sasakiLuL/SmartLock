using MediatR;

namespace SmartLock.Application.Users.Register;

public record RegisterUserCommand(
    string Email, 
    string UserName) : IRequest<Guid>;
