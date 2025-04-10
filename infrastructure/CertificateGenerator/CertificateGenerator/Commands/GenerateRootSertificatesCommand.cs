using PrivateCertificateGenerator.CertificateBuilders;

namespace PrivateCertificateGenerator.Commands;

public class GenerateRootSertificatesCommand : Command, ICommand
{
    public GenerateRootSertificatesCommand() : base("gen-rootCA-serts")
    {
        ParametersKeys.Add("FilePath",
        [
            "-p",
            "--path"
        ]);

        ParametersKeys.Add("RegistrationCode",
        [
            "-reg-code",
            "--registration-code"
        ]);
    }

    public void SetParameters(IEnumerable<string> parameters)
    {
        InitParameters(parameters.ToList());
    }

    public void Execute()
    {
        Console.Write("CountryName: ");
        string countryName = Console.ReadLine() ?? "";
        Console.Write("State: ");
        string state = Console.ReadLine() ?? "";
        Console.Write("City: ");
        string city = Console.ReadLine() ?? "";
        Console.Write("Organization: ");
        string organization = Console.ReadLine() ?? "";
        Console.Write("Organization Unit: ");
        string organizationUnit = Console.ReadLine() ?? "";
        Console.Write("Common Name: ");
        string commonName = Console.ReadLine() ?? "";

        var dn = new DistinguishedName(
            countryName,
            state,
            city,
            organization,
            organizationUnit,
            commonName);

        ICertificateBuilder rootCertificateBuilder = new RootCertificateBuilder(dn);

        var rootCertificate = rootCertificateBuilder.Build();

        ICertificateBuilder verificationCertificateBuilder = new VerificationCertificateBuilder(
            rootCertificate,
            dn,
            Parameters["RegistrationCode"]);

        var verificationCertificate = verificationCertificateBuilder.Build();

        Utils.ExportCertificate(rootCertificate, $"{Parameters["FilePath"]}/rootCA/metadata/", "rootCA");
        Utils.ExportCertificate(verificationCertificate, $"{Parameters["FilePath"]}/rootCA/metadata/", "verificationCert");

        Utils.ExportCertificateToPem(rootCertificate.X509Certificate, $"{Parameters["FilePath"]}/rootCA/", "rootCA");
        Utils.ExportPrivateKeyToPem(rootCertificate.PrivateKey, $"{Parameters["FilePath"]}/rootCA/", "rootCA");

        Utils.ExportCertificateToPem(verificationCertificate.X509Certificate, $"{Parameters["FilePath"]}/rootCA/", "verificationCert");
        Utils.ExportPrivateKeyToPem(verificationCertificate.PrivateKey, $"{Parameters["FilePath"]}/rootCA/", "verificationCert");
    }
}
