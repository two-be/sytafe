using Sytafe.Library.Models;

namespace Sytafe.Library.Extensions;

public static class ExceptionExtensions
{
    public static ExceptionInfo ToInfo(this Exception value)
    {
        return new ExceptionInfo(value);
    }
}