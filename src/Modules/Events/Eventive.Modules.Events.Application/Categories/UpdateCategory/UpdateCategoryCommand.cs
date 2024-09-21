

using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid CategoryId, string Name) : ICommand;
