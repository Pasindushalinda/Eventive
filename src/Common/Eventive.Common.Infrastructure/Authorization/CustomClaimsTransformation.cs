using Eventive.Common.Application.Authorization;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Common.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Eventive.Common.Infrastructure.Authorization;

//When an user logs in, the CustomClaimsTransformation class is triggered.
//This class fetches the user’s permissions from the IPermissionService based
//on their role and dynamically adds these permissions as claims to their ClaimsPrincipal

//This setup ensures that your claims transformation logic is applied to each authenticated user,
//dynamically adding permissions as claims. It’s a solid approach to managing claims and enhancing
//security based on user permissions

//CustomClaimsTransformation class is designed to transform the claims of a ClaimsPrincipal
//by adding additional claims based on user permissions
internal sealed class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        //If the principal already has a Sub claim, it simply returns the principal without any modifications
        if (principal.HasClaim(c => c.Type == CustomClaims.Sub))
        {
            return principal;
        }

        //A new scope is created to retrieve the IPermissionService and get the identity ID from the principal
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        //identityId is user identity in keycloak
        string identityId = principal.GetIdentityId();

        //The GetUserPermissionsAsync method is called to fetch the user’s permissions.
        //If the operation fails, it throws an EventiveException
        Result<PermissionsResponse> result = await permissionService.GetUserPermissionsAsync(identityId);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(IPermissionService.GetUserPermissionsAsync), result.Error);
        }

        //A new ClaimsIdentity is created, and claims are added based on the retrieved permissions.
        //The new identity is then added to the principal
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(CustomClaims.Sub, result.Value.UserId.ToString()));

        foreach (string permission in result.Value.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
