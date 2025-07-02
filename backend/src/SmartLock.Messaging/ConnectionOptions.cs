namespace SmartLock.Messaging;

public class ConnectionOptions
{
    public string Host { get; set; } = default!;

    public int Port { get; set; }

    public string ClientId { get; set; } = default!;

    public string RootCAPath { get; set; } = default!;

    public string CertificatePath { get; set; } = default!;

    public string PrivateKeyPath { get; set; } = default!;
}
