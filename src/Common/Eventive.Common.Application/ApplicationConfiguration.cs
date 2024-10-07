using Eventive.Common.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eventive.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        //Register IRequest and IRequestHandler in MediatR which location is Application layer
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

            //used to register the ExceptionHandlingPipelineBehavior in your application’s dependency injection container.
            //This ensures that the exception handling behavior is applied to all requests handled by MediatR
            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));

            //used to register the RequestLoggingPipelineBehavior in your application’s dependency injection container.
            //This ensures that the logging behavior is applied to all requests handled by MediatR.
            //Registration: By adding RequestLoggingPipelineBehavior to the pipeline, you ensure that every request
            //processed by MediatR will go through this behavior.
            //Logging: This behavior logs the start and end of each request, including any errors that occur,
            //providing valuable insights into the application’s operation.
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));

            //register validation pipeline behavior
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        //Add FluentValidation.DependencyInjectionExtensions registering
        //includeInternalTypes: true -> fluent validation visible only application layer
        //services.AddValidatorsFromAssemblies: Registers all validators from the specified assemblies.
        //moduleAssemblies: The assemblies to scan for validators.
        //includeInternalTypes: true: Includes internal types in the scan.
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}
