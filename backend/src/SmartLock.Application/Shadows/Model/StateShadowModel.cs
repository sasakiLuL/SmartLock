namespace SmartLock.Application.Shadows.Model;

public record StateShadowModel(
    bool? Locked,
    int? Status);
