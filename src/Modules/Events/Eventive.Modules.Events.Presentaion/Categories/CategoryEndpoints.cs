using Microsoft.AspNetCore.Routing;

namespace Eventive.Modules.Events.Presentaion.Categories;

public static class CategoryEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        ArchiveCategory.MapEndpoint(app);
        CreateCategory.MapEndpoint(app);
        GetCategory.MapEndpoint(app);
        GetCategories.MapEndpoint(app);
        UpdateCategory.MapEndpoint(app);
    }
}
