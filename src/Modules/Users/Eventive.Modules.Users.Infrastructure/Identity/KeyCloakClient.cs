using System.Net.Http.Json;

namespace Eventive.Modules.Users.Infrastructure.Identity;

//make api call to keycloak api
internal sealed class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(UserRepresentation user, CancellationToken cancellationToken = default)
    {
        //send post request to user endpoint in keycloak
        //user obj send as serialize json
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);

        //make sure req success or fail
        httpResponseMessage.EnsureSuccessStatusCode();

        //exctract user id from loaction header
        return ExtractIdentityIdFromLocationHeader(httpResponseMessage);
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

        return identityId;
    }
}
