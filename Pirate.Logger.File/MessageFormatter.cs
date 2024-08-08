using Pirate.Logger.File.Extensions;
using Pirate.Logger.File.Models;
using System.Diagnostics;

namespace Pirate.Logger.File;

public class MessageFormatter
{
    public string FormatMessage(LogType logType, string message, Dictionary<string, object> data, Exception? exception = null)
    {
        var time = DateTime.Now.ToString();
        return $"{time.Replace(" uur", "")}: {logType}: {GetCallingClassName()}.cs: {message.ToSingleLine()}";
    }

    public string GetCallingClassName()
    {
        var stackTrace = new StackTrace();
        var callingClass = stackTrace.GetFrame(4)?.GetMethod()?.DeclaringType?.Name ?? "Unknown";

        return callingClass;
    }
}
