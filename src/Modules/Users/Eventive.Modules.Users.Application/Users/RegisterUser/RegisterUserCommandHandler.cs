using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Users.Application.Abstractions.Data;
using Eventive.Modules.Users.Application.Abstractions.Identity;
using Eventive.Modules.Users.Domain.Users;

namespace Eventive.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = await identityProviderService.RegisterUserAsync(
             new UserModel(request.Email, request.Password, request.FirstName, request.LastName),
             cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var user = User.Create(request.Email, request.FirstName, request.LastName, result.Value);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
