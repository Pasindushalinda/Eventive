using Eventive.Modules.Events.Application.Abstarctions.Clock;

namespace Eventive.Modules.Events.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
