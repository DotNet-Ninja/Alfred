using System.IO.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Alfred.Constants;

namespace Alfred.Configuration;

public class AlfredSettings
{
    private readonly IFileSystem _files;
    private readonly JsonSerializerOptions _options;

    public AlfredSettings(): this(new FileSystem())
    {
    }

    public AlfredSettings(IFileSystem files)
    {
        _files = files;
        _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        _options.Converters.Add(new JsonStringEnumConverter());
    }

    public static readonly string SettingsPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Defaults.Notes.SettingsFolderName, Defaults.Notes.SettingsFileName);

    public string NotesPath { get; set; } = Defaults.Notes.NotesPath;

    public string DailyFolderName { get; set; } = Defaults.Notes.DailyFolderName;

    public string TemplateRoot { get; set; } = Defaults.Notes.TemplateRoot;

    [JsonIgnore]
    public string FullDailyPath => Path.Combine(NotesPath, DailyFolderName);
    
    [JsonIgnore]
    public string FullTemplateRoot => Path.Combine(NotesPath, TemplateRoot);

    [JsonIgnore]
    public string AlfredTemplateRoot => Path.Combine(FullTemplateRoot, Defaults.Notes.AlfredTemplateFolderName);

    public string GetTemplateFile(string name)
    {
        return Path.Combine(AlfredTemplateRoot, name);
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(this, _options);
        var file = _files.FileInfo.New(SettingsPath);
        if (!file.Directory!.Exists)
        {
            file.Directory.Create();
        }
        using (var writer = file.CreateText())
        {
            await writer.WriteAsync(json);
        }
    }
}