using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using FluentValidation.Results;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Eventive.Common.Application.Behaviors;

//This class is designed to validate requests before they are processed by the request handler.
//The actual implementation of the Handle method is not provided in the snippet,
//but it would typically iterate over the validators and validate the request.

//IBaseCommand use to identify commands(separated command and queries). This behevior not run against the query.
//queries don't use IBaseCommand
internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationFailure[] validationFailures = await ValidateAsync(request);

        if (validationFailures.Length == 0)
        {
            return await next();
        }

        //if we have validationFailures then short circuit the request
        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type resultType = typeof(TResponse).GetGenericArguments()[0];

            MethodInfo? failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
            {
                return (TResponse)failureMethod.Invoke(null, [CreateValidationError(validationFailures)]);
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
        }

        throw new ValidationException(validationFailures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!validators.Any())
        {
            return [];
        }

        //ValidationContext<TRequest> context: Creates a validation context for the request.
        var context = new ValidationContext<TRequest>(request);

        //ValidateAsync: Validates the request using all available validators.
        //Task.WhenAll: Runs all validators asynchronously.
        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));

        ValidationFailure[] validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }

    //CreateValidationError: Converts validation failures into a ValidationError object.
    //Error.Problem: Creates an error object for each validation failure.
    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
        new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
}
