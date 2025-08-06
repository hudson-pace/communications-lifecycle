namespace CommLifecycle.Api.Services;

public class ServiceException(
  string message,
  ServiceErrorCode errorCode = ServiceErrorCode.Unknown,
  string? userMessage = null,
  Exception? innerException = null) : Exception(message, innerException)
{
  public ServiceErrorCode ErrorCode { get; } = errorCode;
  public string? UserMessage { get; } = userMessage;
  public override string ToString()
  {
    return $"[{ErrorCode}] {Message}" + (UserMessage is not null ? $" (UserMessage: {UserMessage})" : "");
  }
}