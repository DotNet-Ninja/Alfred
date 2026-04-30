using System.CommandLine;
using System.IO.Abstractions;
using System.Text;
using Alfred.Configuration;
using Alfred.Services;
using Microsoft.Extensions.Logging;

namespace Alfred.Commands;

public class NewDailyNoteCommand: Command
{
    private readonly AlfredSettings _settings;
    private readonly ILogger<NewDailyNoteCommand> _logger;
    private readonly ITimeProvider _time;
    private readonly IFileSystem _files;

    private const string TodayMarker = "# Today";
    private const string SectionMarker = "# ";
    private const string TasksMarker = "- [ ] ";
    private const string TomorrowMarker = "# Tomorrow";
    private const string BacklogMarker = "# Backlog";

    public NewDailyNoteCommand(AlfredSettings settings, ILogger<NewDailyNoteCommand> logger, ITimeProvider time, IFileSystem files) 
        : base("daily", "Creates a new daily note for today")
    {
        _settings = settings;
        _logger = logger;
        _time = time;
        _files = files;
        var forceOption = new Option<bool>(["-f", "--Force"], () => false,
            "Overwrite any existing file if it exists.");
        AddOption(forceOption);
        this.SetHandler(InvokeAsync, forceOption);
    }

    public async Task InvokeAsync(bool force)
    {
        var dailyDirectory = Path.Combine(_settings.FullDailyPath, _time.Year.ToString(), _time.Now.ToNumberedMonthString());
        if (!_files.Directory.Exists(dailyDirectory))
        {
            _files.Directory.CreateDirectory(dailyDirectory);
        }

        var noteFile = GetDailyNoteFilePath(_time.Now);
        if (!_files.File.Exists(noteFile) || force)
        {
            
        }
        else
        {
            _logger.LogWarning($"File '{noteFile}' already exists.  Use -f/--Force to overwrite");   
        }
    }

    private string GetDailyNoteFilePath(DateTime date)
    {
        return Path.Combine(_settings.FullDailyPath, date.Year.ToString(), date.ToNumberedMonthString(), $"{date.ToDailyNoteName()}.md");
    }

    public string FindPreviousDailyNote()
    {
        var current = _time.Now.AddDays(-1);
        var minDate = _time.Now.AddDays(-30);
        var latestNotePath = string.Empty;
        while (string.IsNullOrEmpty(latestNotePath) && current > minDate)
        {
            var candidate = GetDailyNoteFilePath(current);
            if (_files.File.Exists(candidate))
            {
                latestNotePath = candidate;
            }
            else
            {
                current = current.AddDays(-1);
            }
        }
        if (string.IsNullOrEmpty(latestNotePath))
        {
            _logger.LogWarning("No previous daily notes found in the last 30 days.");
        }
        else
        {
            _logger.LogInformation($"Found previous daily note: {current.ToDailyNoteName()}");
        }
        return latestNotePath;
    }

    public async Task<List<string>> FindOutstandingTodayTasksAsync(string notePath)
    {
        var tasks = new List<string>();
        using (var reader = _files.File.OpenText(notePath))
        {
            bool completed = false;
            bool found = false;
            while (!completed && !reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())??string.Empty;
                if (found)
                {
                    if (line.Trim().StartsWith(TasksMarker))
                    {
                        tasks.Add(line);
                    }
                }
                completed = line.Trim().StartsWith(SectionMarker, StringComparison.CurrentCulture) && found;
                found = found || line.Trim().Equals(TodayMarker, StringComparison.CurrentCulture);
            }
        }

        return tasks;
    }

    public async Task<List<string>> FindTomorrowTasksAsync(string notePath)
    {
        var tasks = new List<string>();
        using (var reader = _files.File.OpenText(notePath))
        {
            bool completed = false;
            bool found = false;
            while (!completed && !reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync()) ?? string.Empty;
                if (found)
                {
                    if (line.Trim().StartsWith(TasksMarker))
                    {
                        tasks.Add(line);
                    }
                }
                completed = line.Trim().StartsWith(SectionMarker, StringComparison.CurrentCulture) && found;
                found = found || line.Trim().Equals(TomorrowMarker, StringComparison.CurrentCulture);
            }
        }

        return tasks;
    }

    public async Task<List<string>> FindBacklogTasksAsync(string notePath)
    {
        var tasks = new List<string>();
        using (var reader = _files.File.OpenText(notePath))
        {
            bool completed = false;
            bool found = false;
            while (!completed && !reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync()) ?? string.Empty;
                if (found)
                {
                    if (line.Trim().StartsWith(TasksMarker))
                    {
                        tasks.Add(line);
                    }
                }
                completed = line.Trim().StartsWith(SectionMarker, StringComparison.CurrentCulture) && found;
                found = found || line.Trim().Equals(BacklogMarker, StringComparison.CurrentCulture);
            }
        }

        return tasks;
    }
}