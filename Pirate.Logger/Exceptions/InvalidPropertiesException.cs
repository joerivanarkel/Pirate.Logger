namespace Pirate.Logger.Exceptions;

internal class InvalidPropertiesException : Exception
{
    public InvalidPropertiesException(string message) : base(message) { } 

    public InvalidPropertiesException(string message, Exception innerException) : base(message, innerException) { }
}
