using Eventive.Modules.Events.Application.Abstarctions.Messaging;
using Eventive.Modules.Events.Application.Categories.GetCategory;

namespace Eventive.Modules.Events.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;
