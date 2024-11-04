using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Ticketing.Application.Customers.CreateCustomer;
using Eventive.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventive.Modules.Ticketing.Presentation.Customers;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName));

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
