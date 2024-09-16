using Eventive.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Eventive.Modules.Events.Application.Abstarctions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
