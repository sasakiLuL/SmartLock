using Microsoft.Extensions.DependencyInjection;
using SmartLock.Messaging.Consumers;
using SmartLock.Messaging.Producers;

namespace SmartLock.Messaging;

public class MessagingBuilder(IServiceCollection services)
{
    private readonly List<ConsumerDescriptor> _consumers = [];
    private readonly Dictionary<Type, string> _producerTopics = [];
    private readonly ConnectionOptions _connectionOptions = new();

    public void ConsumerTopic<TConsumer, TPayload>(string topicFilter)
        where TConsumer : class, IMessageConsumer<TPayload> where TPayload : class
    {
        services.AddScoped<TConsumer>();
        _consumers.Add(
            new ConsumerDescriptor(topicFilter, typeof(TConsumer), typeof(TPayload)));
    }

    public void MapProducerTopic<TPayload>(string topic)
        where TPayload : class
    {
        _producerTopics[typeof(TPayload)] = topic;
    }

    public void Host(Action<ConnectionOptions> action)
    {
        action(_connectionOptions);
    }

    public void Build()
    {
        var topicProducerRegistry = new TopicProducerRegistry();

        foreach (var kv in _producerTopics)
        {
            var method = typeof(TopicProducerRegistry).GetMethod("Register")!.MakeGenericMethod(kv.Key);
            method.Invoke(topicProducerRegistry, [kv.Value]);
        }

        services.AddSingleton(topicProducerRegistry);
        services.AddSingleton<IMessageProducer, MessageProducer>();

        services.AddSingleton<ConsumerDispatcher>();
        services.AddSingleton(new TopicConsumerRegistry(_consumers));

        var topicsToSubscribe = _consumers.Select(c => c.Topic).Distinct().ToList();
        services.AddSingleton(sp =>
        {
            var dispatcher = sp.GetRequiredService<ConsumerDispatcher>();
            return new MessagingService(_connectionOptions, topicsToSubscribe, dispatcher);
        });
    }
}
