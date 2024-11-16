using Eventive.Common.Application.Exceptions;
using System.Security.Claims;

namespace Eventive.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    //You create extension methods to easily extract user information from the ClaimsPrincipal.
    //These methods help you get the user's ID, identity ID, and permissions from the JWT claims,
    //ensuring that your application can handle user data efficiently.

    //These differences reflect their distinct purposes: one ensures the
    //uniqueness and proper type (GUID) of the user identifier, while the
    //other focuses on retrieving a standard identity ID as a string.
    //Both are essential for robust identity management in your application
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;

        return Guid.TryParse(userId, out Guid parsedUserId) ?
        parsedUserId :
            throw new EventiveException("User identifier is unavailable");
    }

    //Retrieves the NameIdentifier claim value and throws an exception if unavailable
    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
               throw new EventiveException("User identity is unavailable");
    }

    //Extracts all permission claims and returns them as a HashSet<string>.
    //Throws an exception if permissions are unavailable.
    //The use of HashSet helps to eliminate duplicates, ensuring each permission is unique
    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                              throw new EventiveException("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}
