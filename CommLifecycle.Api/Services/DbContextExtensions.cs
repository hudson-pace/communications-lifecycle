using CommLifecycle.Api;
using CommLifecycle.Api.Services;
using Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static async Task<Result> TrySaveAsync(this DbContext context, CancellationToken ct)
  {
    try
    {
      var affected = await context.SaveChangesAsync(ct);
      return Result.Success();
    }
    catch (DbUpdateConcurrencyException ex)
    {
      return Result.Failure(new ServiceException("Concurrency conflict", ServiceErrorCode.Conflict, "Database error. Could not complete operation", ex));
    }
    catch (DbUpdateException ex)
    {
      return Result.Failure(new ServiceException("Database update failed", ServiceErrorCode.Unknown, "Error while saving to database", ex));
    }
    catch (Exception ex)
    {
      return Result.Failure(new ServiceException("Unexpected error during save", ServiceErrorCode.Unknown, "Unexpected error during save", ex));
    }
  }
}