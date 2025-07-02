namespace SmartLock.Application.Shadows.Model;

public record ActionRequestShadowModel(
    Guid ActionId,
    int ActionType,
    string? ActionArguments);
