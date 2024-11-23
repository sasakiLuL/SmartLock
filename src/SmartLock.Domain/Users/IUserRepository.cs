using SmartLock.Domain.Abstractions.Repositories;
using SmartLock.Domain.Users.Emails;

namespace SmartLock.Domain.Users;

public interface IUserRepository : IRepository<User, UserModel>
{
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
}
