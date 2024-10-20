namespace GymAppBackend.Core.Exceptions;

public abstract class CustomException : Exception
{
    public int StatusCode { get; set; }

    protected CustomException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}