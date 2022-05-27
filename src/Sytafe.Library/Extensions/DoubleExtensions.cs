namespace Sytafe.Library.Extensions;

public static class DoubleExtensions
{
    public static int ToInt32(this double value)
    {
        try
        {
            return Convert.ToInt32(value);
        }
        catch
        {
            return 0;
        }
    }
}