using SmartLock.Core.Results;
using FluentValidation;

namespace SmartLock.Core.Entities;

public record ValueObject
{
    protected static Result<TValue> Validate<TValue>(
        AbstractValidator<TValue> validator,
        TValue instance)
    {
        return validator
            .Validate(instance)
            .ToResult(instance);
    }
}
