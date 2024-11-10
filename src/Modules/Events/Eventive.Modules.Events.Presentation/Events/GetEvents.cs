using Eventive.Modules.Events.Application.Events.GetEvents;
using Eventive.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Eventive.Common.Presentation.ApiResults;
using Eventive.Common.Presentation.Endpoints;

namespace Eventive.Modules.Events.Presentation.Events;

internal sealed class GetEvents : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<EventResponse>> result = await sender.Send(new GetEventsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Events);
    }
}
