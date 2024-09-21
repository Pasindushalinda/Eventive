using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Categories.ArchiveCategory;

public sealed record ArchiveCategoryCommand(Guid CategoryId) : ICommand;
