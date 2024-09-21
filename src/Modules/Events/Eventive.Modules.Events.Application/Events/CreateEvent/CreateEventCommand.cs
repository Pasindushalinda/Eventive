using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
    string Title,
    string Description,
    string Location,
    DateTime StartAtUtc,
    DateTime? EndAtUtc) : ICommand<Guid>;
