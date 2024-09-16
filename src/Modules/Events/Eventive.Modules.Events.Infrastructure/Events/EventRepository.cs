using Eventive.Modules.Events.Domain.Events;
using Eventive.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository(EventDbContext context) : IEventRepository
{
    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Event @event)
    {
        context.Events.Add(@event);
    }
}
