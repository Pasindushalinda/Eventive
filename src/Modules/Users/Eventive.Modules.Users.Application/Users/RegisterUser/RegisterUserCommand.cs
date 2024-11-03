using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName)
    : ICommand<Guid>;
