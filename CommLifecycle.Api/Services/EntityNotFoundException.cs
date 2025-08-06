namespace CommLifecycle.Api.Services;

public class EntityNotFoundException : ServiceException
{
  public EntityNotFoundException(string entityType, int entityId) : base(
    $"[{entityType}] with ID {entityId} was not found.",
    ServiceErrorCode.NotFound,
    $"[{entityType}] with ID {entityId} was not found."
    )
  {
  }
}