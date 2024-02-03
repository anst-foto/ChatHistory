using System.Runtime.Serialization;

namespace ChatHistory.Core.Config;

public class ConfigException : Exception
{
    public ConfigException()
    {
    }

    protected ConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ConfigException(string? message) : base(message)
    {
    }

    public ConfigException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
