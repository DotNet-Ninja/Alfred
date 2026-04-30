using System.CommandLine;

namespace Alfred.Commands;

public class SetCommand: Command
{
    public SetCommand(SetNotesPathCommand setNotesPathCommand, SetDailyNotesFolderNameCommand setDailyNotesFolderNameCommand, SetNotesTemplateRootNameCommand setNotesTemplateRootNameCommand) : base("set", "Sets a value in your environment")
    {
        AddCommand(setNotesPathCommand);
        AddCommand(setDailyNotesFolderNameCommand);
        AddCommand(setNotesTemplateRootNameCommand);
    }
}