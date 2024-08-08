using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pirate.Logger.Data;
using Pirate.Logger.Data.Models;
using Pirate.Logger.Exceptions;
using Pirate.Logger.Interfaces;
using Pirate.Logger.Models;

namespace Pirate.Logger;

public class Logger : ILogger
{
    private Repository _repository;
    private readonly LoggerConfiguration _configuration;

    public Logger(Repository repository, LoggerConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Info(string message, Dictionary<string, object> properties)
    {
        if (!Validate(message, properties))
            throw new InvalidPropertiesException($"Invalid properties for message: {message} and properties: {properties}");

        Write(LogType.Info, message, properties);
    }

    /// <inheritdoc />
    public void Warning(string message, Dictionary<string, object> properties)
    {
        if (!Validate(message, properties))
            throw new InvalidPropertiesException($"Invalid properties for message: {message} and properties: {properties}");

        Write(LogType.Warning, message, properties);
    }

    /// <inheritdoc />
    public void Error(string message, Exception exception, Dictionary<string, object> properties)
    {
        if (!Validate(message, properties))
            throw new InvalidPropertiesException($"Invalid properties for message: {message}, properties: {properties} and exception: {exception}");

        Write(LogType.Error, message, properties, exception);
    }



    private bool Write(LogType logType,string message, Dictionary<string, object> properties, Exception? exception = null)
    {
        if (_configuration.Target.UseConsole)
            Console.WriteLine(message);

        if (_configuration.Target.UseFile) { }

        if (_configuration.Target.UseDatabase)
            _repository.AddLogEntry(logType, message, properties, exception);

        return true;
    }

    private bool Validate( string message, Dictionary<string, object> properties, Exception? exception = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;
        

        if (properties == null)
            return false;

        if (exception != null)
        {
            if (exception.Message == null)
                return false;
        }

        return true;
    }
}
