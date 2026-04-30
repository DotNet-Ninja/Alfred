using System.CommandLine;

namespace Alfred.Commands;

public class GetCommand: Command
{
    public GetCommand(GetSettingsCommand getSettings) : base("get", "Gets a value or resource in your environment")
    {
        AddCommand(getSettings);
    }
}