using Eventive.Modules.Events.Application.Categories.GetCategories;
using Eventive.Modules.Events.Application.Categories.GetCategory;
using Eventive.Common.Domain;
using Eventive.Modules.Events.Presentaion.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Eventive.Common.Application.ICacheService;

namespace Eventive.Modules.Events.Presentaion.Categories;

internal static class GetCategories
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender, ICacheService cacheService) =>
        {
            IReadOnlyCollection<CategoryResponse> categoryResponses =
                await cacheService.GetAsync<IReadOnlyCollection<CategoryResponse>>("categories");

            if (categoryResponses is not null)
            {
                return Results.Ok(categoryResponses);
            }

            Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(new GetCategoriesQuery());

            if (result.IsSuccess)
            {
                await cacheService.SetAsync("categories", result.Value);
            }

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Categories);
    }
}
