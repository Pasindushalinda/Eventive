using Eventive.Modules.Events.Application.Abstarctions.Messaging;

namespace Eventive.Modules.Events.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid CategoryId) : IQuery<CategoryResponse>;
