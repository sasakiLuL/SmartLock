using SmartLock.Core.Entities;

namespace SmartLock.Domain.Users.UserIds;

public record UserId(Guid Value) : IEntityId;
