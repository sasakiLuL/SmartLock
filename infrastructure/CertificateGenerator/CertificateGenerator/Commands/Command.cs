namespace PrivateCertificateGenerator.Commands;

public abstract class Command
{
    public string CommandKey { get; protected init; }

    public Dictionary<string, string[]> ParametersKeys { get; protected set; } = [];

    public Dictionary<string, string> Parameters { get; protected set; } = [];

    protected Command(string commandKey)
    {
        CommandKey = commandKey;
    }

    protected void InitParameters(List<string> parameters)
    {
        foreach (var paramKey in ParametersKeys)
        {
            if (!parameters.Any())
            {
                throw new Exception($"Not enought arguments");
            }

            int keyValueIndex = parameters.FindIndex(x => paramKey.Value.Contains(x));

            if (keyValueIndex == -1)
            {
                throw new Exception($"The {paramKey.Key} was not provided");
            }

            if (keyValueIndex == parameters.Count() - 1)
            {
                throw new Exception($"The key value was not provided");
            }

            Parameters.Add(paramKey.Key, parameters[keyValueIndex + 1]);

            parameters.Remove(parameters[keyValueIndex + 1]);
            parameters.Remove(parameters[keyValueIndex]);
        }

        if (parameters.Any())
        {
            throw new Exception($"Too many arguments");
        }
    }
}
