using SmartLock.Domain.Core;

namespace SmartLock.Domain.Core.Exceptions;

public class NotFoundException : Exception
{
    public Error Error { get; }

    public NotFoundException(Error error) : base(error.Message)
    {
        Error = error;
    }
}
