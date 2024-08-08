namespace Pirate.Logger.Interfaces;

public interface ILogger
{
    /// <summary>
    /// Logs an informational message
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="properties">Extra properties to log</param>
    void Info(string message, Dictionary<string, object> properties);

    /// <summary>
    /// Logs a warning message
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="properties">Extra properties to log</param>
    void Warning(string message, Dictionary<string, object> properties);

    /// <summary>
    /// Logs an error message
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    /// <param name="properties">Extra properties to log</param>
    void Error(string message, Exception exception, Dictionary<string, object> properties);

}
