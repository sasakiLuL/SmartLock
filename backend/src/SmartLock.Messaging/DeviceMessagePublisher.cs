using Microsoft.Extensions.Options;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Messaging.Options;
using System.Text.Json;
using System.Text;
using SmartLock.Messaging.Abstractions;
using SmartLock.Application.Devices.ActivationRequest;
using SmartLock.Application.Devices;

namespace SmartLock.Messaging;

public class DeviceMessagePublisher(
    IDeviceMessageService deviceMessageService,
    IOptions<MqttOptions> mqttOptions) : IDeviceMessagePublisher
{
    private readonly MqttOptions _mqttOptions = mqttOptions.Value ?? throw new ArgumentNullException(nameof(mqttOptions));

    public async Task PublishAsync(DeviceMessage message, MessagePolicy policy, CancellationToken cancellationToken = default)
    {
        var policyString = $"{_mqttOptions.PoliciesBase}/{message.HardwareId}/{_mqttOptions.PublisherPolicies[policy.ToString()]}";

        var jsonMessage = policy switch
        {
            MessagePolicy.ActivationRequests => JsonSerializer.Serialize(
                message as ActivationRequestMessage ?? throw new Exception("No message class was found")),
            MessagePolicy.Actions => JsonSerializer.Serialize(
                message as DeviceActionMessage ?? throw new Exception("No message class was found")),
            _ => throw new Exception("No message class was found")
        };

        await deviceMessageService.PublishAsync(policyString, Encoding.UTF8.GetBytes(jsonMessage), cancellationToken);
    }
}
