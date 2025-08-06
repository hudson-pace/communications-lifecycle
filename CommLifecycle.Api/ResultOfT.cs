namespace CommLifecycle.Api;
public record Result<T>(bool IsSuccess, T? Value, Exception? Error)
{
  public static Result<T> Success(T value) => new(true, value, null);
  public static Result<T> Failure(Exception error) => new(false, default, error);

  public TResult Match<TResult>(
      Func<T, TResult> onSuccess,
      Func<Exception, TResult> onFailure)
      => IsSuccess ? onSuccess(Value!) : onFailure(Error!);

  public Result<TResult> Map<TResult>(Func<T, TResult> transform)
      => IsSuccess
          ? Result<TResult>.Success(transform(Value!))
          : Result<TResult>.Failure(Error!);

  public async Task<Result<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> transform)
      => IsSuccess
          ? Result<TResult>.Success(await transform(Value!))
          : Result<TResult>.Failure(Error!);
}