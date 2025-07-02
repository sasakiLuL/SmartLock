namespace SmartLock.Application.Features.Devices;

public record StateResponse(
    bool Locked,
    int Status,
    DateTime LastUpdatedOnUtc);
