namespace SmartLock.Messaging.Consumers;

public record ConsumerDescriptor(
    string Topic,
    Type HandlerType,
    Type PayloadType);
