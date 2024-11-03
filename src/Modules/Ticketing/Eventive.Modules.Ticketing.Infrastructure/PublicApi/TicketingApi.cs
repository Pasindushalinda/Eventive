using Eventive.Modules.Ticketing.Application.Customers.CreateCustomer;
using Eventive.Modules.Ticketing.PublicApi;
using MediatR;

namespace Eventive.Modules.Ticketing.Infrastructure.PublicApi;

internal sealed class TicketingApi(ISender sender) : ITicketingApi
{
    public async Task CreateCustomerAsync(
        Guid customerId,
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(
            new CreateCustomerCommand(customerId, email, firstName, lastName),
            cancellationToken);
    }
}
