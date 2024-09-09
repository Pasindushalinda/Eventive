using Eventive.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventive.Modules.Events.Api.Events;

public static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, EventDbContext context) =>
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartAtUtc = request.StartAtUtc,
                EndAtUtc = request.EndAtUtc,
                Status = EventStatus.Draft,
            };

            context.Events.Add(@event);

            await context.SaveChangesAsync();

            return Results.Ok(@event);
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
    public EventStatus Status { get; set; }
}