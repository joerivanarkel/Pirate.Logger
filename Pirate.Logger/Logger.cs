using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pirate.Logger.Data;
using Pirate.Logger.Data.Models;
using Pirate.Logger.Exceptions;
using Pirate.Logger.File;
using Pirate.Logger.Interfaces;
using Pirate.Logger.Models;
using DataLogType = Pirate.Logger.Data.Models.LogType;
using FileLogType = Pirate.Logger.File.Models.LogType;

namespace Pirate.Logger;

public class Logger : ILogger
{
    private Repository _repository;
    private FileHandler _fileHandler;
    private MessageFormatter _messageFormatter;
    private readonly LoggerConfiguration _configuration;

    public Logger(Repository repository, FileHandler fileHandler, MessageFormatter messageFormatter, LoggerConfiguration configuration)
    {
        _repository = repository;
        _fileHandler = fileHandler;
        _messageFormatter = messageFormatter;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Info(string message, Dictionary<string, object> properties)
    {
        if (!Validate(message, properties))
            throw new InvalidPropertiesException($"Invalid properties for message: {message} and properties: {properties}");

        Write("Info", message, properties);
    }

    /// <inheritdoc />
    public void Warning(string message, Dictionary<string, object> properties)
    {
        if (!Validate(message, properties))
            throw new InvalidPropertiesException($"Invalid properties for message: {message} and properties: {properties}");

        Write("Warning", message, properties);
    }

    /// <inheritdoc />
    public void Error(string message, Exception exception, Dictionary<string, object> properties)
    {
        if (!Validate(message, properties))
            throw new InvalidPropertiesException($"Invalid properties for message: {message}, properties: {properties} and exception: {exception}");

        Write("Error", message, properties, exception);
    }



    private bool Write(string logType,string message, Dictionary<string, object> properties, Exception? exception = null)
    {
        if (_configuration.Target.UseConsole)
            Console.WriteLine(message);

        if (_configuration.Target.UseFile)
            _fileHandler.WriteLine(_messageFormatter.FormatMessage(Enum.Parse<FileLogType>(logType), message, properties, exception));

        if (_configuration.Target.UseDatabase)
            _repository.AddLogEntry(Enum.Parse<DataLogType>(logType), message, properties, exception);

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
