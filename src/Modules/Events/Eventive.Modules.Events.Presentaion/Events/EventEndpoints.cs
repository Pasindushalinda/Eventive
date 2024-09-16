using Microsoft.AspNetCore.Routing;

namespace Eventive.Modules.Events.Presentaion.Events;

public class EventEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
    }
}
