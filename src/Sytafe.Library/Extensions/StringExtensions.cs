using System.Text;

namespace Sytafe.Library.Extensions;

public static class StringExtensions
{
    public static string ToSHA512(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        using (var sha = System.Security.Cryptography.SHA512.Create())
        {
            var utf8Bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha.ComputeHash(utf8Bytes);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}