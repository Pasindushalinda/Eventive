using Microsoft.AspNetCore.Authorization;

namespace Eventive.Common.Infrastructure.Authorization;

//This PermissionRequirement class defines a custom authorization
//requirement for an ASP.NET Core application

//This indicates that PermissionRequirement is an authorization
//requirement used in policy-based authorization.
internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}
