using Eventive.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Eventive.Modules.Events.Application.Abstarctions.Messaging;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
