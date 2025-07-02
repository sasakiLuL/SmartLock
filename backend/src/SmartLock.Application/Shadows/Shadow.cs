using SmartLock.Application.Shadows.Model;

namespace SmartLock.Application.Shadows;

public record DesiredShadow(
    ActionRequestShadowModel? Action);

public record ReportedShadow(
    ActionResponseShadowModel? Action,
    StateShadowModel? State);

public record ShadowState(
    DesiredShadow? Desired,
    ReportedShadow? Reported);

public record Shadow(
    ShadowState State);
