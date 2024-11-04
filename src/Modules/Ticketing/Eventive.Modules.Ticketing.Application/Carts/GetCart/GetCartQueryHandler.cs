using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;

namespace Eventive.Modules.Ticketing.Application.Carts.GetCart;

internal sealed class GetCartQueryHandler(CartService cartService) : IQueryHandler<GetCartQuery, Cart>
{
    public async Task<Result<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        return await cartService.GetAsync(request.CustomerId, cancellationToken);
    }
}
