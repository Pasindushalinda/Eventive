using Eventive.Common.Infrastructure.Interceptors;
using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Users.Application.Abstractions.Data;
using Eventive.Modules.Users.Domain.Users;
using Eventive.Modules.Users.Infrastructure.Database;
using Eventive.Modules.Users.Infrastructure.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Eventive.Modules.Users.Infrastructure.PublicApi;
using Eventive.Modules.Users.PublicApi;

namespace Eventive.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddScoped<IUsersApi, UsersApi>();
    }
}
