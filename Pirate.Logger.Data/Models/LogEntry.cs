namespace Pirate.Logger.Data.Models;

public class LogEntry
{
    public int Id { get; set; }
    public LogType LogType { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }
    public string? ExceptionMessage { get; set; }
    public Dictionary<string, object> Properties { get; set; } = [];
}