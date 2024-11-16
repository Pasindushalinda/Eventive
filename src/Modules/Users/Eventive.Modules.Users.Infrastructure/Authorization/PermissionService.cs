using Eventive.Common.Application.Authorization;
using Eventive.Common.Domain;
using Eventive.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace Eventive.Modules.Users.Infrastructure.Authorization;

//this will call every api call user makes. need to caches this
internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
