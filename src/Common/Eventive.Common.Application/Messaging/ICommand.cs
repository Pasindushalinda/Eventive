using Eventive.Common.Domain;
using MediatR;

namespace Eventive.Common.Application.Messaging;

// Extends IRequest<Result> and IBaseCommand. This interface represents a command that returns a Result.
public interface ICommand : IRequest<Result>, IBaseCommand;

//ICommand<TResponse>: Extends IRequest<Result<TResponse>> and IBaseCommand.
//This interface represents a command that returns a Result<TResponse>.
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

//A marker interface that can be used to identify command types.
public interface IBaseCommand;
