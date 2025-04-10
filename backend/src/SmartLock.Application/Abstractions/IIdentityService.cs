namespace SmartLock.Application.Abstractions;

public interface IIdentityService
{
    Task<bool> IsExistsAsync(Guid identityProviderId, CancellationToken token = default);
}
