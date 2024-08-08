namespace Pirate.Logger.File.Extensions;

internal static class StringExtensions
{

    internal static string ToSingleLine(this string message)
    {
        if (message.Contains('\n')) message = message.Replace('\n', ' ');
        if (message.Contains('\r')) message = message.Replace('\r', ' ');

        return message;
    }
}