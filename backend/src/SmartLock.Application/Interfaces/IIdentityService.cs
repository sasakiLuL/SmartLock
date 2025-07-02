namespace SmartLock.Application.Interfaces;

public interface IIdentityService
{
    Task<bool> IsExistsAsync(Guid identityProviderId, CancellationToken token = default);
}
