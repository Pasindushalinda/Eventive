using Eventive.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Data;

namespace Eventive.Common.Application.Behaviors;

//this is middleware
//for logging perpose install Serilog

//The IPipelineBehavior<TRequest, TResponse> interface in MediatR allows you to define custom behaviors that can
//be executed before and after a request is handled.This is useful for implementing cross-cutting concerns like
//logging, validation, and error handling

//ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger: Injects a logger for logging information
//where TRequest : class: Constrains TRequest to be a class.
//where TResponse : Result: Constrains TResponse to be of type Result.
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string moduleName = GetModuleName(typeof(TRequest).FullName!);
        string requestName = typeof(TRequest).Name;

        //This line adds a property named “Module” with the value of moduleName to the log context.
        //This property will be included in all log entries within this using block
        using (LogContext.PushProperty("Module", moduleName))
        {
            logger.LogInformation("Processing request {RequestName}", requestName);

            //Calls the next delegate in the pipeline, which processes the request and returns a response of type TResponse
            TResponse result = await next();

            if (result.IsSuccess)
            {
                logger.LogInformation("Completed request {RequestName}", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Completed request {RequestName} with error", requestName);
                }
            }

            return result;
        }
    }

    private static string GetModuleName(string requestName) => requestName.Split('.')[2];
}
