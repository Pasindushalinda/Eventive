using Eventive.Common.Infrastructure.Outbox;
using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Attendance.Application.Abstractions.Authentication;
using Eventive.Modules.Attendance.Application.Abstractions.Data;
using Eventive.Modules.Attendance.Domain.Attendees;
using Eventive.Modules.Attendance.Domain.Events;
using Eventive.Modules.Attendance.Domain.Tickets;
using Eventive.Modules.Attendance.Infrastructure.Attendees;
using Eventive.Modules.Attendance.Infrastructure.Authentication;
using Eventive.Modules.Attendance.Infrastructure.Database;
using Eventive.Modules.Attendance.Infrastructure.Events;
using Eventive.Modules.Attendance.Infrastructure.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventive.Modules.Attendance.Infrastructure;

public static class AttendanceModule
{
    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AttendanceDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Attendance))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessageInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AttendanceDbContext>());

        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        services.AddScoped<IAttendanceContext, AttendanceContext>();
    }
}