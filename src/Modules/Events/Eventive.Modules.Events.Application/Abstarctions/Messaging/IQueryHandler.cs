using Eventive.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Eventive.Modules.Events.Application.Abstarctions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
