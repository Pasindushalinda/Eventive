﻿using Eventive.Modules.Attendance.Domain.Tickets;
using Eventive.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Attendance.Infrastructure.Tickets;

internal sealed class TicketRepository(AttendanceDbContext context) : ITicketRepository
{
    public async Task<Ticket?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Tickets.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public void Insert(Ticket ticket)
    {
        context.Tickets.Add(ticket);
    }
}
