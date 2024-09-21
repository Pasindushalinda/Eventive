using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
