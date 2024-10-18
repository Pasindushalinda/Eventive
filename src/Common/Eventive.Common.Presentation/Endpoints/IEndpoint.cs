using Microsoft.AspNetCore.Routing;

namespace Eventive.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

