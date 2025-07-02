using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Features.Users;
using SmartLock.Domain.ValueObjects.EmailAddresses;
using SmartLock.Domain.ValueObjects.Usernames;

namespace SmartLock.DataAccessLayer.Features.Users;

public class UserRepository(SmartLockContext smartLockContext) : Repository<User, UserModel>(smartLockContext), IUserRepository
{
    public async Task<bool> IsEmailUniqueAsync(EmailAddress email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AllAsync(x => x.Email != email, cancellationToken);
    }

    public async Task<bool> IsUsernameUniqueAsync(Username username, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AllAsync(x => x.Username != username, cancellationToken);
    }

    public async Task<User?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        return user is not null ? new User(user) : null;
    }

    public async Task<User?> ReadByIdentityProviderIdAsync(Guid identityProviderId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.IdentityProviderId == identityProviderId, cancellationToken);

        return user is not null ? new User(user) : null;
    }
}
