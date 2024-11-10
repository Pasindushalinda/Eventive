using Microsoft.Extensions.DependencyInjection;

namespace Eventive.Common.Infrastructure.Authentication;

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
