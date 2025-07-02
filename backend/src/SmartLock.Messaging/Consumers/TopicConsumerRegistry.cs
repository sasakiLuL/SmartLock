namespace SmartLock.Messaging.Consumers;

public class TopicConsumerRegistry(List<ConsumerDescriptor> consumerDescriptors)
{
    public ConsumerDescriptor? ResolveConsumer(string receivedTopic, out Guid hardwareId)
    {
        foreach (var desc in consumerDescriptors)
        {
            if (TryMatchAndExtractId(desc.Topic, receivedTopic, out hardwareId))
                return desc;
        }

        hardwareId = default;
        return null;
    }

    private static bool TryMatchAndExtractId(string policy, string received, out Guid hardwareId)
    {
        hardwareId = default;
        var policyParts = policy.Split('/');
        var receivedParts = received.Split('/');

        if (policyParts.Length != receivedParts.Length)
            return false;

        for (int i = 0; i < policyParts.Length; i++)
        {
            if (policyParts[i] == "+")
            {
                if (!Guid.TryParse(receivedParts[i], out hardwareId))
                    return false;
            }
            else if (policyParts[i] != receivedParts[i])
            {
                return false;
            }
        }

        return true;
    }
}
