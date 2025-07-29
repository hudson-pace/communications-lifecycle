using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;
using SharedModels.Models;

namespace MoviesApi.Tests;

public class Tests
{
    private ICommunicationService _communicationService;
    private CommunicationType _testType;

    [SetUp]
    public async Task Setup()
    {
        DbContextOptions<MoviesApiContext> options = new DbContextOptionsBuilder<MoviesApiContext>()
            .UseInMemoryDatabase("testDbName")
            .Options;

        MoviesApiContext context = new MoviesApiContext(options);
        _testType = new()
        {
            Name = "TestType"
        };
        context.CommunicationTypes.Add(_testType);
        await context.SaveChangesAsync();

        SeedDb s = new();
        await s.Seed(context);

        _communicationService = new CommunicationService(context);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public async Task CreateCommunicationAsync_WhenMissingField_ShouldThrowDbUpdateException()
    {
        Communication Communication = new Communication();

        await Assert.ThatAsync(
            async () => await _communicationService.CreateCommunicationAsync(Communication),
            Throws.TypeOf<DbUpdateException>().With.Message.StartWith("Required properties") // ie "Required properties '{'Title'}' are missing for the instance...
        );
    }
    [Test]
    public async Task CreateCommunicationAsync_WhenSuccessful_ShouldReturnCreatedCommunication()
    {
        Communication Communication = new()
        {
            Title = "EOB TEST",
            Type = _testType,
        };
        Communication ReturnedCommunication = await _communicationService.CreateCommunicationAsync(Communication);
        Assert.That(Communication, Is.EqualTo(ReturnedCommunication));
    }
}
