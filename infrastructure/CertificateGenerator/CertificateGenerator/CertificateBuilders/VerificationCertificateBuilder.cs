using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PrivateCertificateGenerator.CertificateBuilders;

public class VerificationCertificateBuilder : ICertificateBuilder
{
    public VerificationCertificateBuilder(Certificate rootCertificate, DistinguishedName distinguishedName, string registrationCode)
    {
        RootCertificate = rootCertificate;
        RegistrationCode = registrationCode;
        DistinguishedName = distinguishedName;
    }

    public DistinguishedName DistinguishedName { get; set; }

    public Certificate RootCertificate { get; set; }

    public string RegistrationCode { get; set; }

    public Certificate Build()
    {
        using RSA verificationRsa = RSA.Create(2048);

        X500DistinguishedNameBuilder distinguishedNameBuilder = new X500DistinguishedNameBuilder();

        distinguishedNameBuilder.AddCountryOrRegion(DistinguishedName.CountryName);
        distinguishedNameBuilder.AddStateOrProvinceName(DistinguishedName.State);
        distinguishedNameBuilder.AddLocalityName(DistinguishedName.City);
        distinguishedNameBuilder.AddOrganizationName(DistinguishedName.Organization);
        distinguishedNameBuilder.AddOrganizationalUnitName(DistinguishedName.OrganizationUnit);
        distinguishedNameBuilder.AddCommonName(RegistrationCode);

        CertificateRequest verificationCsr = new CertificateRequest(
            distinguishedNameBuilder.Build(),
            verificationRsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );

        using var rootCAKey = RSA.Create();

        rootCAKey.ImportPkcs8PrivateKey(RootCertificate.PrivateKey, out _);

        //sign verification certificate with RootCA
        X509Certificate2 verificationCert = verificationCsr.Create(
            X509CertificateLoader.LoadCertificate(RootCertificate.X509Certificate).SubjectName,
            X509SignatureGenerator.CreateForRSA(rootCAKey, RSASignaturePadding.Pkcs1),
            DateTimeOffset.UtcNow,
            DateTimeOffset.UtcNow.AddDays(500),
            GenerateSerialNumber());

        return new Certificate(
            verificationRsa.ExportPkcs8PrivateKey(), 
            new DistinguishedName("", "", "", "", "", RegistrationCode), 
            verificationCert.RawData);
    }

    private static byte[] GenerateSerialNumber()
    {
        byte[] serial = new byte[16];
        RandomNumberGenerator.Fill(serial);
        return serial;
    }
}
