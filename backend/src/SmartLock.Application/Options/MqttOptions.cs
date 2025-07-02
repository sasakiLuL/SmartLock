namespace SmartLock.Application.Options;

public class MqttOptions
{
    public static readonly string Section = "DeviceMessageBroker";

    public string ClientId { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public int Port { get; set; }

    public Dictionary<string, string> ConsumerPolicies { get; set; } = [];

    public Dictionary<string, string> ProducerPolicies { get; set; } = [];

    public string CertificatePath { get; set; } = string.Empty;

    public string PrivateKeyPath { get; set; } = string.Empty;

    public string RootCAPath { get; set; } = string.Empty;
}
