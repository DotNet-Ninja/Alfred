using System.CommandLine;
using Alfred.Configuration;
using Alfred.Constants;
using Microsoft.Extensions.Logging;

namespace Alfred.Commands;

public class SetDailyNotesFolderNameCommand : Command
{
    private readonly AlfredSettings _settings;
    private readonly ILogger _logger;

    public SetDailyNotesFolderNameCommand(AlfredSettings settings, ILogger<SetDailyNotesFolderNameCommand> logger) : base("daily-notes-folder-name", "Sets the name of your daily notes folder in your vault")
    {
        _settings = settings;
        _logger = logger;
        var pathOptions = new Option<string>(["-n", "--Name"], () => Defaults.Notes.DailyFolderName,
            "Name of Daily Notes folder.");
        AddOption(pathOptions);
        this.SetHandler(InvokeAsync, pathOptions);
    }

    public async Task InvokeAsync(string path)
    {
        _settings.DailyFolderName = path;
        await _settings.SaveAsync();
        _logger.LogInformation("Daily Notes folder name set to: {Path}", path);
    }
}