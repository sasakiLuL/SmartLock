namespace SmartLock.Domain.Exceptions;

public class BadRequestException : Exception
{
    public static readonly string ValidationErrorMessage = "The validation errors were occured.";

    public Error Error { get; }

    public BadRequestException(string message, Error error) : base(message)
    {
        Error = error;
    }

    public BadRequestException(Error error) : base(error.Message)
    {
        Error = error;
    }
}
