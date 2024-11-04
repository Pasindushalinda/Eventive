using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
