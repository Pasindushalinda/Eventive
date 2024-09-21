using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eventive.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        //Register IRequest and IRequestHandler in MediatR which loaction is Application layer
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
        });

        //Add FluentValidation.DependencyInjectionExtensions registering
        //includeInternalTypes: true -> fluent validation visible only application layer
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}
