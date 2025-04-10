using System.Security.Cryptography.X509Certificates;

namespace PrivateCertificateGenerator.CertificateBuilders;

public interface ICertificateBuilder
{
    Certificate Build();
}
