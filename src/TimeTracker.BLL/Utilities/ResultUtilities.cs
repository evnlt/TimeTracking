namespace TimeTracker.BLL.Utilities;

public static class ResultUtilities
{
    public static Result<T> As<T>(this Result result)
    {
        return new Result<T>
        {
            IsSuccess = result.IsSuccess,
            ErrorType = result.ErrorType,
            Message = result.Message
        };
    }
}