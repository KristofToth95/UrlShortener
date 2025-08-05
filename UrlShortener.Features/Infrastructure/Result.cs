namespace UrlShortener.Features.Infrastructure;

public class Result<T>
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public T? Value { get; }

    private Result(T? value,bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value,true, Error.None);
    public static Result<T> Failure(Error error) => new(error);
}