namespace SmartLock.Application.Shadows.Model;

public record ActionResponseShadowModel(
    Guid LastExecutedActionId,
    int LastExecutedActionStatus,
    long LastExecutedAt);
