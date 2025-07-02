using SmartLock.Domain.Entities;
using SmartLock.Domain.ValueObjects.EmailAddresses;
using SmartLock.Domain.ValueObjects.Usernames;

namespace SmartLock.Domain.Features.Users;

public class UserModel : AggregateRoot
{
    public required Guid IdentityProviderId { get; set; }

    public required EmailAddress Email { get; set; }

    public required Username Username { get; set; }

    public static UserModel Create(
        Guid identityProviderId,
        EmailAddress emailAddress,
        Username userName)
    {
        return new UserModel
        {
            Id = Guid.NewGuid(),
            IdentityProviderId = identityProviderId,
            Email = emailAddress,
            Username = userName
        };
    }
}
