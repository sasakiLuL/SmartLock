namespace SmartLock.Api.Features.Users.Register;

public record RegisterUserRequest(
    string Email,
    string Username);
