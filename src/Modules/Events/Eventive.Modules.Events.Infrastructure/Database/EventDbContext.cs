using Eventive.Common.Infrastructure.Outbox;
using Eventive.Modules.Events.Application.Abstarctions.Data;
using Eventive.Modules.Events.Domain.Categories;
using Eventive.Modules.Events.Domain.Events;
using Eventive.Modules.Events.Domain.TicketTypes;
using Eventive.Modules.Events.Infrastructure.Events;
using Eventive.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Events.Infrastructure.Database;

public sealed class EventDbContext(DbContextOptions<EventDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Event> Events { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<TicketType> TicketTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Event);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
    }
}
