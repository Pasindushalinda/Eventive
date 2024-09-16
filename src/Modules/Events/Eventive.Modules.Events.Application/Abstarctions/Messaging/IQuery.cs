using Eventive.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Eventive.Modules.Events.Application.Abstarctions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
