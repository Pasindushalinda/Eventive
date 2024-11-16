using Eventive.Common.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Eventive.Common.Infrastructure.Authorization;

//When an user attempts to access a resource, the PermissionAuthorizationHandler
//comes into play. It checks whether the user's claims include the necessary
//permission to access that resource. For instance, to view a confidential report,
//a user might need the ViewConfidentialReports permission

//The AuthorizationHandler processes the authorization logic. If the user’s claims
//meet the required permission, access is granted. Otherwise, access is denied, and
//the user is informed they lack the necessary permissions.

//This class inherits from AuthorizationHandler<PermissionRequirement>,
//making it an authorization handler for PermissionRequirement.

//Authorization Logic: This handler checks if the current user has the required
//permission specified in the PermissionRequirement. If the user has the permission,
//it marks the requirement as succeeded, allowing access to the resource

//This setup ensures that your custom PermissionAuthorizationHandler
//is invoked when the PermissionPolicy is applied
internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    //This method is overridden to implement the logic for handling the PermissionRequirement
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        //Retrieves the permissions of the current user using the GetPermissions extension method
        HashSet<string> permissions = context.User.GetPermissions();

        //If the user’s permissions include the required permission, the requirement is marked as succeeded.
        //set in RequireAuthorization("users:read")
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
