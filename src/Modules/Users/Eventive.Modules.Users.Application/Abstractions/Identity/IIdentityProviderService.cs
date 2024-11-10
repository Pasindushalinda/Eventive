using Eventive.Common.Domain;

namespace Eventive.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    //send request to key cloak api
    Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
}
