using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using CommLifecycle.Api.Services;
using NUnit.Framework;
using SharedModels.Models;
using SharedModels.DTOs;

namespace CommLifecycle.Api.Tests;

public class ResultTests
{
  private CommunicationTypeDto validDto;
  [SetUp]
  public void Setup()
  {
    validDto = new () { Id=1, Name="TestDto", Statuses=[] };
  }

  [Test]
  public void From_ShouldWrapSuccessResultWithValue()
  {
    var baseResult = Result.Success();

    var wrapped = baseResult.From(validDto);

    Assert.Multiple(() =>
    {
      Assert.That(wrapped.IsSuccess, Is.True);
      Assert.That(wrapped.Value, Is.EqualTo(validDto));
      Assert.That(wrapped.Error, Is.Null);
    });
  }
  [Test]
  public void From_ShouldWrapFailureResultWithException()
  {
    var ex = new InvalidOperationException("Something went wrong");
    var baseResult = Result.Failure(ex);

    var wrapped = baseResult.From(validDto);

    Assert.Multiple(() =>
    {
      Assert.That(wrapped.IsSuccess, Is.False);
      Assert.That(wrapped.Error, Is.EqualTo(ex));
      Assert.That(wrapped.Value, Is.Null);
    });
  }
  [Test]
  public void Success_ShouldCreateSuccessfulResultWithValue()
  {
    var result = Result<CommunicationTypeDto>.Success(validDto);

    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Value, Is.EqualTo(validDto));
      Assert.That(result.Error, Is.Null);
    });
  }
  [Test]
  public void Failure_ShouldCreateFailedResultWithException()
  {
    var ex = new ArgumentNullException("dto");
    var result = Result<CommunicationDto>.Failure(ex);

    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.False);
      Assert.That(result.Error, Is.EqualTo(ex));
      Assert.That(result.Value, Is.Null);
    });
  }
}