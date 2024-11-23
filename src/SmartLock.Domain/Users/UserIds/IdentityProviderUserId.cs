using SmartLock.Core.Entities;

namespace SmartLock.Domain.Users.UserIds;

public record IdentityProviderUserId(Guid Value) : IEntityId;
