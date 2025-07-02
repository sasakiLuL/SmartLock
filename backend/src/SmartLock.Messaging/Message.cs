namespace SmartLock.Messaging;

public record Message<TPayload>(
    Guid HardwareId,
    TPayload Payload);
