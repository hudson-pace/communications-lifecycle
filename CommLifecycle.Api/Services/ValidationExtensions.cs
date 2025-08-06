using System.ComponentModel.DataAnnotations;

namespace CommLifecycle.Api.Services;

public static class ValidationExtensions
{
  public static Result Validate(this object instance)
  {
    var context = new ValidationContext(instance);
    var results = new List<ValidationResult>();

    bool isValid = Validator.TryValidateObject(instance, context, results, validateAllProperties: true);
    if (isValid) return Result.Success();

    Dictionary<string, List<string>> errorDict = results
      .SelectMany(r => r.MemberNames.Select(name => new { name, r.ErrorMessage }))
      .Where(x => !string.IsNullOrWhiteSpace(x.ErrorMessage))
      .GroupBy(x => x.name)
      .ToDictionary(
        g => g.Key,
        g => g.Select(x => x.ErrorMessage!.Trim()).Distinct().ToList()
      );
    return Result.Failure(new ValidationException(errorDict));
  }
}