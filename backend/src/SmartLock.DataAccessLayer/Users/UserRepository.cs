using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Users;

namespace SmartLock.DataAccessLayer.Users;

public class UserRepository : Repository<User, UserModel>, IUserRepository
{
    public UserRepository(SmartLockContext smartLockContext) : base(smartLockContext)
    {
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.UserModels.AllAsync(x => x.Email != email, cancellationToken);
    }

    public async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.UserModels.AllAsync(x => x.Username != username, cancellationToken);
    }

    public async Task<User?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var userModel = await _context.UserModels.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (userModel is null)
        {
            return null;
        }

        return CreateEntityFromModel(userModel);
    }

    public async Task<User?> ReadByIdentityProviderIdAsync(Guid identityProviderId, CancellationToken cancellationToken = default)
    {
        var userModel = await _context.UserModels.FirstOrDefaultAsync(x => x.IdentityProviderId == identityProviderId, cancellationToken);

        if (userModel is null)
        {
            return null;
        }

        return CreateEntityFromModel(userModel);
    }
}
