using Alfred.Configuration;
using System.CommandLine;
using Alfred.Services;
using Microsoft.Extensions.Logging;

namespace Alfred.Commands;

public class GetSettingsCommand: Command
{
    private readonly AlfredSettings _settings;
    private readonly ILogger _console;
    private readonly IObjectSerializer _serializer;

    public GetSettingsCommand(AlfredSettings settings, ILogger<GetSettingsCommand> console, IObjectSerializer serializer) : base("settings", "Gets Alfred settings")
    {
        _settings = settings;
        _console = console;
        _serializer = serializer;
        this.SetHandler(InvokeAsync);
    }

    public Task InvokeAsync()
    {
        _console.LogInformation($"Alfred Settings File: {AlfredSettings.SettingsPath}");
        var json = _serializer.Serialize(_settings)??"{}";
        _console.LogInformation(json);
        return Task.CompletedTask;
    }
}