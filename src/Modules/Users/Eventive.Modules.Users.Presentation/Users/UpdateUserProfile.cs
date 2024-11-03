using Eventive.Common.Domain;
using Eventive.Common.Presentation.ApiResults;
using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Users.Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventive.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id}/profile", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateUserCommand(
                id,
                request.FirstName,
                request.LastName));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
