namespace SmartLock.Domain.Core;

public abstract record ValueObject<TValue>
{
    public TValue Value { get; }

    protected ValueObject(TValue value)
    {
        Value = value;
    }
}
