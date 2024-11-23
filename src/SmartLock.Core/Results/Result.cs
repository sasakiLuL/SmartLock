using SmartLock.Core.Errors;

namespace SmartLock.Core.Results;

public class Result
{
    protected Result(bool isSuccess, Error[] errors)
    {
        if (isSuccess && errors.Any())
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && !errors.Any())
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error[] Errors { get; }

    public static Result Success() => new(true, []);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, []);

    public static Result Failure(Error[] errors) => new(false, errors);

    public static Result Failure(Error error) => new(false, [error]);

    public static Result<TValue> Failure<TValue>(Error[] errors) => new(default!, false, errors);

    public static Result<TValue> Failure<TValue>(Error error) => new(default!, false, [error]);

    public static Result AllFailuresOrSuccess(params Result[] results)
    {
        var errors = results.SelectMany(x => x.Errors);

        if (errors.Any())
        {
            return Failure([.. errors]);
        }

        return Success();
    }
}
