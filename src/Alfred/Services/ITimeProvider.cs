namespace Alfred.Services;

public interface ITimeProvider
{
    public DateTime Now { get; }

    public int Year { get; }
    public int Month { get; }

}