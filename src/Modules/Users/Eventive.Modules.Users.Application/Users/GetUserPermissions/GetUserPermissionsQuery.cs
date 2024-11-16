using Eventive.Common.Application.Authorization;
using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
