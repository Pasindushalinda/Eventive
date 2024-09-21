using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
