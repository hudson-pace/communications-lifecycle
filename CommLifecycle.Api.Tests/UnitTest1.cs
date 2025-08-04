using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using CommLifecycle.Api.Services;
using NUnit.Framework.Internal;
using SharedModels.Models;
using SharedModels.DTOs;

namespace CommLifecycle.Api.Tests;

public class Tests
{
    private ICommunicationService _communicationService;
    private CommunicationType _testType;

    [SetUp]
    public async Task Setup()
    {
        DbContextOptions<CommLifecycleApiContext> options = new DbContextOptionsBuilder<CommLifecycleApiContext>()
            .UseInMemoryDatabase("testDbName")
            .Options;

        CommLifecycleApiContext context = new CommLifecycleApiContext(options);
        _testType = new()
        {
            Name = "TestType"
        };
        context.CommunicationTypes.Add(_testType);
        await context.SaveChangesAsync();
        await SeedDb.Seed(context);

        _communicationService = new CommunicationService(context, NullLogger<CommunicationService>.Instance);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public async Task CreateCommunicationAsync_WhenMissingField_ShouldThrowDbUpdateException()
    {
        CommunicationDto communicationDto = new();

        await Assert.ThatAsync(
            async () => await _communicationService.CreateCommunicationAsync(communicationDto),
            Throws.TypeOf<DbUpdateException>().With.Message.StartWith("Required properties") // ie "Required properties '{'Title'}' are missing for the instance...
        );
    }
    [Test]
    public async Task CreateCommunicationAsync_WhenSuccessful_ShouldReturnCreatedCommunication()
    {
        /*        
        CommunicationDto communicationDto = new()
        {
            Title = "EOB TEST",
            Type = _testType.ToDto(),
        };
        Communication ReturnedCommunication = await _communicationService.CreateCommunicationAsync(communicationDto);
        Assert.That(communicationDto, Is.EqualTo(ReturnedCommunication));
        */
    }
}
