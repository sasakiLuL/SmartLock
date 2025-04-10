using SmartLock.Domain.Core;

namespace SmartLock.Domain.Users;

public interface IUserRepository : IRepository<User, UserModel>
{
    Task<User?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<User?> ReadByIdentityProviderIdAsync(Guid identityProviderId, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default);
}

