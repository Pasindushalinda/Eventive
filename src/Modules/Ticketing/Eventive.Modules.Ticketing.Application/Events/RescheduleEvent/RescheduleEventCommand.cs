﻿using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
