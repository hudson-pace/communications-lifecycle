namespace CommLifecycle.Api.Services;
public class ResultConversionException : ServiceException
{
  public ResultConversionException() : base(
    "Cannot convert failed Result to Result<T>: missing exception",
    ServiceErrorCode.MissingException)
  {
  }
}