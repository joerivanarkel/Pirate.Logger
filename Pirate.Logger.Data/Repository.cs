using Pirate.Logger.Data.Models;

namespace Pirate.Logger.Data;

public class Repository
{
    private AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public bool AddLogEntry(LogType logType, string message, Dictionary<string, object> data, Exception? exception = null)
    {
        LogEntry logEntry = new()
        {
            LogType = logType,
            Message = message,
            TimeStamp = DateTime.Now,
            ExceptionMessage = exception?.Message,
            Properties = data
        };

        _context.Logs.Add(logEntry);
        _context.SaveChanges();

        return true;
    }
}
