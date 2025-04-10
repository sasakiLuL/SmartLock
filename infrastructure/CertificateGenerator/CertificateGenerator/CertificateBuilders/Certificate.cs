namespace PrivateCertificateGenerator.CertificateBuilders;

public record Certificate(
    byte[] PrivateKey,
    DistinguishedName DistinguishedName,
    byte[] X509Certificate);
