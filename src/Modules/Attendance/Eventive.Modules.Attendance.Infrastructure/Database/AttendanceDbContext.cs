using Eventive.Common.Infrastructure.Inbox;
using Eventive.Common.Infrastructure.Outbox;
using Eventive.Modules.Attendance.Application.Abstractions.Data;
using Eventive.Modules.Attendance.Domain.Attendees;
using Eventive.Modules.Attendance.Domain.Events;
using Eventive.Modules.Attendance.Domain.Tickets;
using Eventive.Modules.Attendance.Infrastructure.Attendees;
using Eventive.Modules.Attendance.Infrastructure.Events;
using Eventive.Modules.Attendance.Infrastructure.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Attendance.Infrastructure.Database;

public sealed class AttendanceDbContext(DbContextOptions<AttendanceDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Attendee> Attendees { get; set; }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Attendance);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
    }
}
