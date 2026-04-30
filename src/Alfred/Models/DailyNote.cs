namespace Alfred.Models;

public class DailyNote
{
    public DailyNote(string filePath, IEnumerable<Line> lines)
    {
        FilePath = filePath;
        Lines = lines.ToList();
    }

    public string FilePath { get; }
    public IReadOnlyList<Line> Lines { get; }


}