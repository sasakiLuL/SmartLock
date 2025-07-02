namespace SmartLock.Messaging.Producers;

public class TopicProducerRegistry
{
    private readonly Dictionary<Type, string> _topicMap = [];

    public void Register<TPayload>(string topicTemplate)
        where TPayload : class
    {
        _topicMap[typeof(TPayload)] = topicTemplate;
    }

    public string GetTopicFor<TPayload>(Guid hardwareId)
        where TPayload : class
    {
        if (!_topicMap.TryGetValue(typeof(TPayload), out var template))
            throw new InvalidOperationException($"No topic mapping for message type {typeof(TPayload).Name}");

        return template.Replace("+", hardwareId.ToString());
    }
}
