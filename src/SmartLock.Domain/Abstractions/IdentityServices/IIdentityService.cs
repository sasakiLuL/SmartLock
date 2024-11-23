namespace SmartLock.Domain.Abstractions.IdentityServices;

public interface IIdentityProvider
{
    Guid UserId { get; }
}
