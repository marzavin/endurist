namespace Endurist.Common.Exceptions;

public class ApplicationConfigurationException : ApplicationException
{
    public ApplicationConfigurationException()
        : this("Application configuration is invalid.")
    { }

    public ApplicationConfigurationException(string message)
        : base(message)
    { }
}