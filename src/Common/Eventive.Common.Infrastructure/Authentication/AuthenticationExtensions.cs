using Microsoft.Extensions.DependencyInjection;

namespace Eventive.Common.Infrastructure.Authentication;

//You add a method to register and configure authentication services in your
//application's service collection. This includes setting up JWT bearer
//authentication, adding authorization, and configuring options for JWT
//bearer authentication using a class

//install Microsoft.AspNetCore.Authentication.JwtBearer
//after this add Authentication middleware to program.cs
internal static class AuthenticationExtensions
{
    //register in InfrastructureConfiguration
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        //not provide jwt bearer options using delegate.(jwtTokenParameters)
        services.AddAuthentication().AddJwtBearer();

        //use to fetch current user
        services.AddHttpContextAccessor();

        //provide jwt bearer options using a class
        services.ConfigureOptions<JwtBearerConfigureOptions>();

        return services;
    }
}
