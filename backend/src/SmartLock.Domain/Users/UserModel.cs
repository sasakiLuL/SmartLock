using SmartLock.Domain.Core;

namespace SmartLock.Domain.Users;

public class UserModel : Model
{
    internal UserModel() { }

    public Guid IdentityProviderId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
}
