namespace PrivateCertificateGenerator.Commands;

public interface ICommand
{
    void Execute();

    void SetParameters(IEnumerable<string> parameters);
}
