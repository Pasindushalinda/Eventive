using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
