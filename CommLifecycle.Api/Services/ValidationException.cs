namespace CommLifecycle.Api.Services;

public class ValidationException : ServiceException
{
    public Dictionary<string, List<string>> Errors { get; }

    public ValidationException(Dictionary<string, List<string>> errors)
        : base("Validation failed", ServiceErrorCode.ValidationFailed, "Please correct the highlighted fields.")
    {
        Errors = errors;
    }
}