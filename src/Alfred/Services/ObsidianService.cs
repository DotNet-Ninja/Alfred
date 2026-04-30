using System.IO.Abstractions;
using Alfred.Configuration;
using Alfred.Models;
using Microsoft.Extensions.Logging;

namespace Alfred.Services;

public class ObsidianService
{
    public ObsidianService(AlfredSettings settings, IFileSystem files, ILogger<ObsidianService> logger)
    {
        _settings = settings;
        _files = files;
        _logger = logger;
    }

    private readonly AlfredSettings _settings;
    private readonly IFileSystem _files;
    private readonly ILogger<ObsidianService> _logger;

    public async Task<List<Line>> GetLines(string filePath)
    {
        var lines = new List<Line>();
        if (!_files.File.Exists(filePath))
        {
            _logger.LogWarning("File not found: {FilePath}", filePath);
            return lines;
        }
        
        var fileLines = await _files.File.ReadAllLinesAsync(filePath);
        for (int i = 0; i < fileLines.Length; i++)
        {
            lines.Add(new Line(i + 1, fileLines[i]));
        }

        return lines;
    }

    public async Task<DailyNote> GetDailyNote(string filePath)
    {
        var lines = await GetLines(filePath);
        return new DailyNote(filePath, lines);
    }
}