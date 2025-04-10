using Newtonsoft.Json;
using PrivateCertificateGenerator.CertificateBuilders;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PrivateCertificateGenerator;

public static class Utils
{
    public static byte[] GenerateSerialNumber()
    {
        byte[] serial = new byte[16];
        RandomNumberGenerator.Fill(serial);
        return serial;
    }

    public static void ExportCertificate(this Certificate certificate, string outputDir, string name)
    {
        Directory.CreateDirectory(outputDir);

        var certificateJson = JsonConvert.SerializeObject(certificate);

        File.WriteAllText($"{outputDir}/{name}_certificate.json", certificateJson);
    }

    public static void ExportCertificateToPem(byte[] rawData, string outputDir, string name)
    {
        Directory.CreateDirectory(outputDir);

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("-----BEGIN CERTIFICATE-----");
        builder.AppendLine(Convert.ToBase64String(rawData, Base64FormattingOptions.InsertLineBreaks));
        builder.AppendLine("-----END CERTIFICATE-----");

        File.WriteAllText($"{outputDir}/{name}.pem", builder.ToString());
    }

    public static void ExportPrivateKeyToPem(byte[] privateKey, string outputDir, string name)
    {
        Directory.CreateDirectory(outputDir);

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("-----BEGIN PRIVATE KEY-----");
        builder.AppendLine(Convert.ToBase64String(privateKey, Base64FormattingOptions.InsertLineBreaks));
        builder.AppendLine("-----END PRIVATE KEY-----");

        File.WriteAllText($"{outputDir}/{name}.key", builder.ToString());
    }
}
