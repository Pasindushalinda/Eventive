﻿using Eventive.Modules.Attendance.Infrastructure.Database;
using Eventive.Modules.Events.Infrastructure.Database;
using Eventive.Modules.Ticketing.Infrastructure.Database;
using Eventive.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Api.Extensions;

public static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<EventDbContext>(scope);
        ApplyMigration<UsersDbContext>(scope);
        ApplyMigration<TicketingDbContext>(scope);
        ApplyMigration<AttendanceDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
