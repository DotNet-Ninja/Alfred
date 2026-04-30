namespace Alfred.Models;

public class Line
{
    public Line(int number, string original)
    {
        Number = number;
        Original = original;
    }

    public int Number { get; }
    public string Original { get; }

    public string Content => string.IsNullOrWhiteSpace(Original) ? string.Empty : Original.TrimStart();

    public int Indentation => Original.Length - Content.Length;
    public bool IsEmpty => string.IsNullOrWhiteSpace(Original);
    public bool IsHeader => Content.StartsWith("#");
    public bool IsListItem => Content.StartsWith("- ") || Content.StartsWith("* ") || Content.StartsWith("+ ");
    public bool IsTask => Content.StartsWith("- [ ] ") || Content.StartsWith("* [ ] ") || Content.StartsWith("+ [ ] ") || Content.StartsWith("- [x] ") || Content.StartsWith("* [x] ") || Content.StartsWith("+ [x] ");

    public bool IsCompletedTask => Content.StartsWith("- [x] ") || Content.StartsWith("* [x] ") || Content.StartsWith("+ [x] ");
    public bool IsUncompletedTask => Content.StartsWith("- [ ] ") || Content.StartsWith("* [ ] ") || Content.StartsWith("+ [ ] ");

    public bool HasDueDate => Content.Contains("📅");
    public bool HasCompletionDate => Content.Contains("✅");
    public bool HasScheduleDate => Content.Contains("⏰");
}