using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Users.Application.Abstractions.Data;
using Eventive.Modules.Users.Domain.Users;
using Eventive.Modules.Users.Infrastructure.Database;
using Eventive.Modules.Users.Infrastructure.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Eventive.Modules.Users.Application.Abstractions.Identity;
using Eventive.Modules.Users.Infrastructure.Identity;
using Microsoft.Extensions.Options;
using Eventive.Common.Application.Authorization;
using Eventive.Modules.Users.Infrastructure.Authorization;
using Eventive.Common.Infrastructure.Outbox;
using Eventive.Modules.Users.Infrastructure.Outbox;

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
        services.AddScoped<IPermissionService, PermissionService>();

        //set concrete values from user appsetting file
        services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        //AddHttpClient: Registers an HttpClient for KeyCloakClient.

        //Lambda Expression: Configures the HttpClient using the serviceProvider to get
        //required services and configure the base address from KeyCloakOptions

        //AddHttpMessageHandler: Adds a custom handler (KeyCloakAuthDelegatingHandler)
        //to handle authentication for HTTP requests.
        services
            .AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
            {
                KeyCloakOptions keyCloakOptions = serviceProvider
                    .GetRequiredService<IOptions<KeyCloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();

        services.AddDbContext<UsersDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessageInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));

        //trigger ConfigureProcessOutboxJob
        services.ConfigureOptions<ConfigureProcessOutboxJob>();

    }
}
