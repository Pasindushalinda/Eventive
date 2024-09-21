using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
