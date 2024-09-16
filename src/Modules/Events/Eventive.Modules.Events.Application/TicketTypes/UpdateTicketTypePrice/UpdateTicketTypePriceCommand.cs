using Eventive.Modules.Events.Application.Abstarctions.Messaging;

namespace Eventive.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

public sealed record UpdateTicketTypePriceCommand(Guid TicketTypeId, decimal Price) : ICommand;
