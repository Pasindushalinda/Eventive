using Eventive.Common.Application.Exceptions;
using Eventive.Common.Infrastructure.Authentication;
using Eventive.Modules.Ticketing.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Eventive.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : ICustomerContext
{
    public Guid CustomerId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new EventiveException("User identifier is unavailable");
}
