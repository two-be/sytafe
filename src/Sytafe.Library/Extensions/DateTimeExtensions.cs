namespace Sytafe.Library.Extensions;

public static class DateTimeExtensions
{
    public static string ToDateTimeString(this DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm");
    }
}