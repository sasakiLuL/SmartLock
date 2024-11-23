using SmartLock.Core.Entities;

namespace SmartLock.Domain.Users;

public class User(UserModel model) : Entity<UserModel>(model)
{
}
