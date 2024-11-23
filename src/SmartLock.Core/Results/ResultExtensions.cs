using FluentValidation.Results;
using FluentValidation;
using SmartLock.Core.Errors;

namespace SmartLock.Core.Results;

public static class ResultExtensions
{
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error[], T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Errors);
    }

    public static Error ToError(this ValidationFailure failure)
    {
        return new Error(failure.ErrorCode, failure.ErrorMessage);
    }

    public static Result ToResult(this ValidationResult result)
    {
        return result.IsValid ?
            Result.Success() :
            Result.Failure(result.Errors.Select(e => e.ToError()).ToArray());
    }

    public static Result<TValue> ToResult<TValue>(this ValidationResult result, TValue value)
    {
        return result.IsValid ?
            Result.Success(value) :
            Result.Failure<TValue>(result.Errors.Select(e => e.ToError()).ToArray());
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder,
        Error failure)
    {
        return ruleBuilder.WithErrorCode(failure.Code).WithMessage(failure.Message);
    }
}
