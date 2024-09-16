namespace Eventive.Modules.Events.Application.Abstarctions.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
