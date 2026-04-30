using System.Globalization;

namespace Alfred;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a DateTime to a string formatted for Obsidian daily notes (YYYY-MM-DD).
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>A string in format YYYY-MM-DD</returns>
    public static string ToObsidianString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// Converts a DateTime to a string formatted for Obsidian daily note names with the day of the week (YYYY-MM-DD - DayOfWeek).   Used for month directories
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>A string in the format MM - MonthName</returns>
    public static string ToNumberedMonthString(this DateTime dateTime)
    {
        return $"{dateTime.Month.ToString().PadLeft(2, '0')} - {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month)}";
    }

    /// <summary>
    /// Converts a DateTime to a string formatted for Obsidian daily note names, including the day of the week.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>A string formatted as YYYY-MM-DD - DayOfWeek</returns>
    public static string ToDailyNoteName(this DateTime dateTime)
    {
        return $"{dateTime.ToObsidianString()} - {dateTime.ToString("dddd", CultureInfo.CurrentCulture)}";
    }
}