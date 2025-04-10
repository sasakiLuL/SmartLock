using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PrivateCertificateGenerator.CertificateBuilders;

public class RootCertificateBuilder : ICertificateBuilder
{
    public DistinguishedName DistinguishedName { get; set; }

    public RootCertificateBuilder(DistinguishedName distinguishedName)
    {
        DistinguishedName = distinguishedName;
    }

    public Certificate Build()
    {
        using RSA rootRsa = RSA.Create(2048);

        X500DistinguishedNameBuilder distinguishedNameBuilder = new X500DistinguishedNameBuilder();

        distinguishedNameBuilder.AddCountryOrRegion(DistinguishedName.CountryName);
        distinguishedNameBuilder.AddStateOrProvinceName(DistinguishedName.State);
        distinguishedNameBuilder.AddLocalityName(DistinguishedName.City);
        distinguishedNameBuilder.AddOrganizationName(DistinguishedName.Organization);
        distinguishedNameBuilder.AddOrganizationalUnitName(DistinguishedName.OrganizationUnit);
        distinguishedNameBuilder.AddCommonName(DistinguishedName.CommonName);

        var x500DistinguishedName = distinguishedNameBuilder.Build();

        CertificateRequest rootCsr = new CertificateRequest(
            x500DistinguishedName,
            rootRsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        rootCsr.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));

        rootCsr.CertificateExtensions.Add(new X509KeyUsageExtension(
            X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, 
            true));

        X509Certificate2 rootCert = rootCsr.CreateSelfSigned(
            DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(10));

        return new Certificate(rootRsa.ExportPkcs8PrivateKey(), DistinguishedName, rootCert.RawData);
    }
}
