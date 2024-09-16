﻿using Eventive.Modules.Events.Application.Abstarctions.Messaging;

namespace Eventive.Modules.Events.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
