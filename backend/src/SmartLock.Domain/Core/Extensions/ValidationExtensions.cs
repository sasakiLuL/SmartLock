using FluentValidation;
using SmartLock.Domain.Core;

namespace SmartLock.Domain.Core.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder,
        Error failure)
    {
        return ruleBuilder.WithErrorCode(failure.Code).WithMessage(failure.Message);
    }
}
