using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
