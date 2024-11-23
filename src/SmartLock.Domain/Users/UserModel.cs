using SmartLock.Core.Entities;
using SmartLock.Domain.Users.Emails;
using SmartLock.Domain.Users.UserIds;

namespace SmartLock.Domain.Users;

public class UserModel : IModel
{
    public required UserId Id { get; init; }

    public required Email Email { get; set; }

    public required IdentityProviderUserId IdentityProviderUserId { get; set; }
}
