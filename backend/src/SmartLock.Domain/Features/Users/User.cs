using SmartLock.Domain.Entities;

namespace SmartLock.Domain.Features.Users;

public class User(UserModel model) : IDomainEntity<UserModel>
{
    public UserModel Model => model;
}
