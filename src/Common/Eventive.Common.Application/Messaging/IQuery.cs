using Eventive.Common.Domain;
using MediatR;

namespace Eventive.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
