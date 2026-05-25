using Microsoft.AspNetCore.Mvc;
using TimeTracker.BLL;

namespace TimeTracker.API.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return result.ErrorType switch
        {
            ErrorType.Validation => new BadRequestObjectResult(result.Message),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(result.Message),
            ErrorType.Forbidden => new ForbidResult(),
            ErrorType.NotFound => new NotFoundObjectResult(result.Message),
            ErrorType.Conflict => new ConflictObjectResult(result.Message),
            ErrorType.ExternalService => new ObjectResult(result.Message) { StatusCode = 503 },
            _ => new ObjectResult(result.Message) { StatusCode = 500 }
        };
    }

    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return result.ErrorType switch
        {
            ErrorType.Validation => new BadRequestObjectResult(result.Message),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(result.Message),
            ErrorType.Forbidden => new ForbidResult(),
            ErrorType.NotFound => new NotFoundObjectResult(result.Message),
            ErrorType.Conflict => new ConflictObjectResult(result.Message),
            ErrorType.ExternalService => new ObjectResult(result.Message) { StatusCode = 503 },
            _ => new ObjectResult(result.Message) { StatusCode = 500 }
        };
    }
    
    public static Result<TTarget> ToResponse<TSource, TTarget>(
        this Result<TSource> result,
        Func<TSource, TTarget> mapper)
    {
        return new Result<TTarget>
        {
            IsSuccess = result.IsSuccess,
            ErrorType = result.ErrorType,
            Message = result.Message,
            Value = result.Value != null ? mapper(result.Value) : default
        };
    }
}