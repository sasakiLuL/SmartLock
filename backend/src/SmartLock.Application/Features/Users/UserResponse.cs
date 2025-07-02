namespace SmartLock.Application.Features.Users;

public record UserResponse(
    Guid Id,
    string Email,
    string Username);
