using System.CommandLine;

namespace Alfred.Commands;

public class NewCommand: Command
{
    public NewCommand(NewDailyNoteCommand dailyCommand) : base("new", "Creates a new item")
    {
        AddCommand(dailyCommand);
    }
}