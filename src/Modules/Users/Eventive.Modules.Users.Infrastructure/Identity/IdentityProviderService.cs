using Eventive.Common.Domain;
using Eventive.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Eventive.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(KeyCloakClient keyCloakClient, ILogger<IdentityProviderService> logger)
    : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        //CredentialRepresentation -> key cload doc
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);
        //using false in CredentialRepresentation, don't need set the new password once athe in keycloak

        try
        {
            //send post req to keycloak
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        //ensure exception happen because of this error
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
