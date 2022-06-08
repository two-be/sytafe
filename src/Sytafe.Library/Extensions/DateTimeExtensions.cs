namespace Sytafe.Library.Extensions;

public static class DateTimeExtensions
{
    public static string ToDateTimeString(this DateTime value)
    {
        return value == DateTime.MinValue ? string.Empty : value.ToString("yyyy-MM-dd HH:mm");
    }
}