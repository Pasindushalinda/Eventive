using System.Diagnostics.CodeAnalysis;

namespace Eventive.Common.Domain;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        //checks if the combination of isSuccess and error is valid. If not, it throws an ArgumentException
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    //Creates a successful Result with no error
    public static Result Success() => new(true, Error.None);

    //Creates a successful Result<TValue> with the provided value
    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    // Creates a failed Result with the specified error
    public static Result Failure(Error error) => new(false, error);

    //Creates a failed Result<TValue> with the specified error
    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    //The [NotNull] attribute in C# is used to indicate that a particular field, parameter, property, or return value should never be null
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<TValue> ValidationFailure(Error error) =>
        new(default, false, error);
}
