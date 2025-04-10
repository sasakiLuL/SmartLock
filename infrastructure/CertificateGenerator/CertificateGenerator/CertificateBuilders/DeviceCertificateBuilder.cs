using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PrivateCertificateGenerator.CertificateBuilders;

public class DeviceCertificateBuilder : ICertificateBuilder
{
    public DeviceCertificateBuilder(Certificate rootCertificate, Guid deviceId)
    {
        RootCertificate = rootCertificate;
        DeviceId = deviceId;
    }

    public Certificate RootCertificate { get; set; }

    public Guid DeviceId { get; set; }

    public Certificate Build()
    {
        using RSA deviceRsa = RSA.Create(2048);

        X500DistinguishedNameBuilder distinguishedNameBuilder = new X500DistinguishedNameBuilder();

        distinguishedNameBuilder.AddCountryOrRegion(RootCertificate.DistinguishedName.CountryName);
        distinguishedNameBuilder.AddStateOrProvinceName(RootCertificate.DistinguishedName.State);
        distinguishedNameBuilder.AddLocalityName(RootCertificate.DistinguishedName.City);
        distinguishedNameBuilder.AddOrganizationName(RootCertificate.DistinguishedName.Organization);
        distinguishedNameBuilder.AddOrganizationalUnitName(RootCertificate.DistinguishedName.OrganizationUnit);
        distinguishedNameBuilder.AddCommonName(DeviceId.ToString());

        CertificateRequest deviceCsr = new CertificateRequest(
            distinguishedNameBuilder.Build(),
            deviceRsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );

        using var rootCAKey = RSA.Create();

        rootCAKey.ImportPkcs8PrivateKey(RootCertificate.PrivateKey, out _);

        X509Certificate2 deviceCert = deviceCsr.Create(
            X509CertificateLoader.LoadCertificate(RootCertificate.X509Certificate).SubjectName,
            X509SignatureGenerator.CreateForRSA(rootCAKey, RSASignaturePadding.Pkcs1),
            DateTimeOffset.UtcNow,
            DateTimeOffset.UtcNow.AddYears(1),
            Utils.GenerateSerialNumber());

        return new Certificate(
            deviceRsa.ExportPkcs8PrivateKey(), 
            RootCertificate.DistinguishedName with 
            { 
                CommonName = DeviceId.ToString() 
            }, 
            deviceCert.RawData);
    }
}
