using Pirate.Logger.File.Extensions;
using Pirate.Logger.File.Models;
using System.Diagnostics;
using System.Text;

namespace Pirate.Logger.File;

public class MessageFormatter
{
    public string FormatMessage(LogType logType, string message, Dictionary<string, object> data, Exception? exception = null)
    {
        var time = DateTime.Now.ToString();
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine($"{time}: {logType}: {GetCallingClassName()}.cs: {message.ToSingleLine()}");
        if (exception != null)
        {
            stringBuilder.AppendLine($"\tException: {exception.Message}");
            if (exception.StackTrace != null && exception.StackTrace.Length > 0)
                stringBuilder.AppendLine($"\tStackTrace: {exception.StackTrace}");
        }
        if (data.Count > 0)
        {
            stringBuilder.AppendLine("\tData:");
            foreach (var item in data)
            {
                stringBuilder.AppendLine($"\t\t{item.Key}: {item.Value}");
            }
        }

        return stringBuilder.ToString();
    }

    public string GetCallingClassName()
    {
        var stackTrace = new StackTrace();
        var callingClass = stackTrace.GetFrame(4)?.GetMethod()?.DeclaringType?.Name ?? "Unknown";

        return callingClass;
    }
}
