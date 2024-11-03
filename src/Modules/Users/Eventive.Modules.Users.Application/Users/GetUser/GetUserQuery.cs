using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
