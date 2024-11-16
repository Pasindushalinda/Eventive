using Eventive.Common.Domain;

namespace Eventive.Common.Application.Authorization;

//other module use this when use microservices
public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
