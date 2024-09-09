using Eventive.Modules.Events.Api.Events;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Events.Api.Database;

public sealed class EventDbContext(DbContextOptions<EventDbContext> options)
    : DbContext(options)
{
    internal DbSet<Event> Events { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Event);
    }
}
