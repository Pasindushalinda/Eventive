using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
