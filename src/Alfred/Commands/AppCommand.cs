using System.CommandLine;

namespace Alfred.Commands;

public class AppCommand: RootCommand
{
    public AppCommand(GetCommand getCommand, SetCommand setCommand, NewCommand newCommand) : base("Alfred - Your local system butler")
    {
        AddCommand(getCommand);
        AddCommand(setCommand);
        AddCommand(newCommand);
    }
}