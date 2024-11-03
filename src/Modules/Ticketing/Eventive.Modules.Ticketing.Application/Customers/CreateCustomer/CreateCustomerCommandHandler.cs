using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Ticketing.Application.Abstractions.Data;
using Eventive.Modules.Ticketing.Domain.Customers;

namespace Eventive.Modules.Ticketing.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.CustomerId, request.Email, request.FirstName, request.LastName);

        customerRepository.Insert(customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
