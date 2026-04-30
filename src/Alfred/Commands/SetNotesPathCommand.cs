using System.CommandLine;
using Alfred.Configuration;
using Alfred.Constants;
using Microsoft.Extensions.Logging;

namespace Alfred.Commands;

public class SetNotesPathCommand : Command
{
    private readonly AlfredSettings _settings;
    private readonly ILogger<SetNotesPathCommand> _logger;

    public SetNotesPathCommand(AlfredSettings settings, ILogger<SetNotesPathCommand> logger) : base("notes-path", "Sets the path to your local note taking app working directory.")
    {
        _settings = settings;
        _logger = logger;
        var pathOptions = new Option<string>(["-n", "--Name"], () => Defaults.Notes.NotesPath,
            "Path to your local note taking app working directory.");
        AddOption(pathOptions);
        this.SetHandler(InvokeAsync, pathOptions);
    }

    public async Task InvokeAsync(string path)
    {
        _settings.NotesPath = path;
        await _settings.SaveAsync();
        _logger.LogInformation("Notes path set to: {Path}", path);
    }
}