using System.CommandLine;
using Alfred.Configuration;
using Alfred.Constants;
using Microsoft.Extensions.Logging;

namespace Alfred.Commands;

public class SetNotesTemplateRootNameCommand : Command
{
    private readonly AlfredSettings _settings;
    private readonly ILogger<SetNotesTemplateRootNameCommand> _logger;

    public SetNotesTemplateRootNameCommand(AlfredSettings settings, ILogger<SetNotesTemplateRootNameCommand> logger) : base("notes-template-folder", "Sets the name of your templates folder in your vault")
    {
        _settings = settings;
        _logger = logger;
        var pathOptions = new Option<string>(["-n", "--Name"], () => Defaults.Notes.TemplateRoot,
            "Name of Template folder.");
        AddOption(pathOptions);
        this.SetHandler(InvokeAsync, pathOptions);
    }

    public async Task InvokeAsync(string path)
    {
        _settings.TemplateRoot = path;
        await _settings.SaveAsync();
        _logger.LogInformation("Notes Template folder name set to: {Path}", path);
    }
}