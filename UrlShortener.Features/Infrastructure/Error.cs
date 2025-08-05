namespace UrlShortener.Features.Infrastructure;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4,
    Argument = 5
}

public record Error(string Description, ErrorType Type)
{
    public string Description { get; }

    public ErrorType Type { get; }
    
    public static readonly Error None = new(string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new(
        "Null value was provided",
        ErrorType.Failure);

    public static Error Failure(string description) =>
        new(description, ErrorType.Failure);

    public static Error NotFound(string description) =>
        new(description, ErrorType.NotFound);

    public static Error Problem(string description) =>
        new(description, ErrorType.Problem);

    public static Error Conflict(string description) =>
        new(description, ErrorType.Conflict);
    
    public static Error Argument(string description) =>
        new(description, ErrorType.Argument);
}
