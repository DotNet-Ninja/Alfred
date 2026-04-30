using System.CommandLine.IO;
using System.IO.Abstractions;
using Alfred.Commands;
using Alfred.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;

namespace Alfred.Configuration;

public class ServiceConfiguration: ServiceConfigurationBase
{
    public override void ConfigureServices()
    {
        Services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole(options => options.FormatterName = MessageOnlyConsoleFormatter.Name);
            builder.AddConsoleFormatter<MessageOnlyConsoleFormatter, SimpleConsoleFormatterOptions>(options =>
            {
                options.IncludeScopes = false;
                options.SingleLine = true;
                options.TimestampFormat = null;
            });
        });
        Services.AddSingleton<ILogger>(serviceProvider => serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Alfred"));
        var configuration = (new ConfigurationBuilder())
                            .AddJsonFile(AlfredSettings.SettingsPath, true)
                            .AddEnvironmentVariables()
                            .AddUserSecrets<ServiceConfiguration>().Build();
        var settings = new AlfredSettings();
        configuration.Bind(settings);

        //Services.AddHttpClient();

        AddSingleton(settings);
        AddSingleton<IObjectSerializer, JsonObjectSerializer>();
        AddSingleton<IJsonObjectSerializer, JsonObjectSerializer>();
        AddSingleton<IFileSystem, FileSystem>();
        AddSingleton<ITimeProvider, SystemTimeProvider>();

        AddSingleton<AppCommand>();
        AddSingleton<SetCommand>();
        AddSingleton<SetNotesPathCommand>();
        AddSingleton<SetNotesTemplateRootNameCommand>();
        AddSingleton<SetDailyNotesFolderNameCommand>();
        //AddSingleton<NewCommand>();
        //AddSingleton<NewDailyNoteCommand>();
        AddSingleton<GetCommand>();
        AddSingleton<GetSettingsCommand>();
        //AddSingleton<ExtractCommand>();
        //AddSingleton<ExtractWebContentCommand>();
    }
}