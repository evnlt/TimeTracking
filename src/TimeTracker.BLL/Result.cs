namespace TimeTracker.BLL;

public class Result
{
    public bool IsSuccess { get; init; }

    public string? Message { get; init; }

    public ErrorType? ErrorType { get; init; }

    public static Result Ok() => new() { IsSuccess = true };

    public static Result Fail(string message, ErrorType type)
        => new() { IsSuccess = false, Message = message, ErrorType = type };
}

public class Result<T> : Result
{
    public T? Value { get; init; }

    public static Result<T> Ok(T value)
        => new() { IsSuccess = true, Value = value };

    public new static Result<T> Fail(string message, ErrorType type)
        => new() { IsSuccess = false, Message = message, ErrorType = type };
    
    public Result<TOut> Map<TOut>(Func<T, TOut> mapper)
    {
        if (!IsSuccess)
        {
            return Result<TOut>.Fail(Message!, ErrorType!.Value);
        }

        return Result<TOut>.Ok(mapper(Value!));
    }
}

public enum ErrorType
{
    Validation,
    Unauthorized,
    Forbidden,
    NotFound,
    Conflict,
    ExternalService,
    Unexpected
}