namespace URLShortener.Application.Exceptions;

public class ConflictException(string error) : Exception
{
    public string Error { get; } = error;
}