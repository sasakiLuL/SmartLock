using SmartLock.Domain.Interfaces;
using SmartLock.Domain.ValueObjects.EmailAddresses;
using SmartLock.Domain.ValueObjects.Usernames;

namespace SmartLock.Domain.Features.Users;

public interface IUserRepository : IRepository<User, UserModel>
{
    Task<User?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<User?> ReadByIdentityProviderIdAsync(Guid identityProviderId, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(EmailAddress email, CancellationToken cancellationToken = default);

    Task<bool> IsUsernameUniqueAsync(Username username, CancellationToken cancellationToken = default);
}

