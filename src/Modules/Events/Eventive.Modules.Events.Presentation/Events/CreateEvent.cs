using Eventive.Modules.Events.Application.Events.CreateEvent;
using Eventive.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Eventive.Common.Presentation.ApiResults;
using Eventive.Common.Presentation.Endpoints;

namespace Eventive.Modules.Events.Presentation.Events;

internal sealed class CreateEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, ISender sender) =>
        {
            var command = new CreateEventCommand(
                request.Title,
                request.Description,
                request.Location,
                request.StartAtUtc,
                request.EndAtUtc);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Events);
    }
}

public sealed class Request
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartAtUtc { get; set; }
    public DateTime EndAtUtc { get; set; }
}