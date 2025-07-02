namespace SmartLock.Domain.ValueObjects;

public abstract record ValueObject<TValue>
{
    public TValue Value { get; }

    protected ValueObject(TValue value)
    {
        Value = value;
    }
}
