using SmartLock.Domain.Core;
using SmartLock.Domain.Shared.EmailAddresses;
using SmartLock.Domain.Shared.Usernames;

namespace SmartLock.Domain.Users;

public class User : Entity<UserModel>
{
    private User(UserModel userModel) : base(userModel) { }

    public Guid Id { get => _model.Id; }

    public Guid IdentityProviderId { get => _model.IdentityProviderId; }

    public EmailAddress Email 
    { 
        get => new EmailAddress(_model.Email); 
        set => _model.Email = value.Value; 
    }

    public Username Username 
    { 
        get => new Username(_model.Username); 
        set => _model.Username = value.Value;
    }

    public static User Create(
        Guid id,
        Guid identityProviderId,
        EmailAddress emailAddress,
        Username userName)
    {
        var model = new UserModel()
        {
            Id = id,
            IdentityProviderId = identityProviderId,
            Email = emailAddress.Value,
            Username = userName.Value
        };

        return new User(model);
    }
}
