namespace CommLifecycle.Api;
using Services;

public record Result(bool IsSuccess, Exception? Exception)
{
  public bool IsFailure => !IsSuccess;
  public static Result Success() => new(true, null);
  public static Result Failure(Exception ex) => new(false, ex);
  public Result<T> From<T>(T successPayload)
  {
    if (IsSuccess) return Result<T>.Success(successPayload);
    return Result<T>.Failure(Exception ?? new ResultConversionException());
  }
}