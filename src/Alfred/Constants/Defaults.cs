using System.Text.Json;
using System.Text.Json.Serialization;

namespace Alfred.Constants;

public static class Defaults
{
    public static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = true
    };

    public static class Notes
    {
        public static readonly string NotesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "notes");
        public static readonly string DailyFolderName = "Daily Notes";
        public static readonly string TemplateRoot = $"Metadata{Path.DirectorySeparatorChar}Templates";
        public const string SettingsFileName = "settings.json";
        public const string SettingsFolderName = ".alfred";
        public const string AlfredTemplateFolderName = "alfred";
    }
}