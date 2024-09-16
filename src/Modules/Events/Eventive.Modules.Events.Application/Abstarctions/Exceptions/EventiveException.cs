using Eventive.Modules.Events.Domain.Abstractions;

namespace Eventive.Modules.Events.Application.Abstarctions.Exceptions;

public sealed class EventiveException : Exception
{
    public EventiveException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
