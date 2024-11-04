using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Carts.RemoveItemFromCart;

public sealed record RemoveItemFromCartCommand(Guid CustomerId, Guid TicketTypeId) : ICommand;
