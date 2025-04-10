namespace SmartLock.Application.Users;

public record UserResponse(
    Guid Id,
    string Email,
    string Username);
