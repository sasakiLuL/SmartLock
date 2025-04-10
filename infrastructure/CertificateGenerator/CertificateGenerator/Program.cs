using PrivateCertificateGenerator.Commands;

Dictionary<string, ICommand> commands = [];

commands.Add("gen-rootCA-certs", new GenerateRootSertificatesCommand());
commands.Add("gen-device-cert", new GenerateDeviceCeritificateCommand());

List<string> argsList = [..args]; 

if (!args.Any())
{
    Console.WriteLine("The command was not provided");
    return -1;
}

ICommand? command = commands[argsList.First()];

if (command is null)
{
    Console.WriteLine("The command was not founded");
    return -1;
}

argsList.Remove(argsList.First());

try
{
    command.SetParameters(argsList);
    command.Execute();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message.ToString());
    return -1;
}

return 0;