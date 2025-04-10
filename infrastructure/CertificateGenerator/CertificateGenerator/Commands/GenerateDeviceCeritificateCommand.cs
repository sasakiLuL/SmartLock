using Newtonsoft.Json;
using PrivateCertificateGenerator.CertificateBuilders;

namespace PrivateCertificateGenerator.Commands;

public class GenerateDeviceCeritificateCommand : Command, ICommand
{
    public GenerateDeviceCeritificateCommand() : base("gen-device-sert")
    {
        ParametersKeys.Add("FilePath",
        [
            "-p",
            "--path"
        ]);

        ParametersKeys.Add("RootCertificatePath",
        [
            "-cert",
            "--root-certificate"
        ]);
    }

    public void SetParameters(IEnumerable<string> parameters)
    {
        InitParameters(parameters.ToList());
    }

    public void Execute()
    {
        if (!File.Exists(Parameters["RootCertificatePath"]))
        {
            throw new Exception($"File does not exist");
        }

        var rootSertificateFileString = File.ReadAllText(Parameters["RootCertificatePath"]);

        Certificate? rootCertificate = JsonConvert.DeserializeObject<Certificate>(rootSertificateFileString);

        if (rootCertificate is null)
        {
            throw new Exception("Cant convert specified file to certificate object");
        }
        var id = Guid.NewGuid();

        ICertificateBuilder deviceCertificateBuilder = new DeviceCertificateBuilder(rootCertificate, id);

        var deviceCert = deviceCertificateBuilder.Build();

        Utils.ExportCertificate(deviceCert, $"{Parameters["FilePath"]}/{id}/metadata/", id.ToString());

        Utils.ExportPrivateKeyToPem(deviceCert.PrivateKey, $"{Parameters["FilePath"]}/{id}/", id.ToString());

        Utils.ExportCertificateToPem(deviceCert.X509Certificate, $"{Parameters["FilePath"]}/{id}/", id.ToString());
    }
}
