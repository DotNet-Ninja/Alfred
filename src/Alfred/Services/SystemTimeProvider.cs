namespace Alfred.Services;

public class SystemTimeProvider: ITimeProvider
{
    public DateTime Now { get; } = DateTime.Now;
    public int Year => Now.Year;
    public int Month => Now.Month;
}