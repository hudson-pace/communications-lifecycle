using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;
using SharedModels.Models;

namespace MoviesApi.Tests;

public class Tests
{
    private ICommunicationService _communicationService;

    [SetUp]
    public void Setup()
    {
        DbContextOptions<MoviesApiContext> options = new DbContextOptionsBuilder<MoviesApiContext>()
            .UseInMemoryDatabase("testDbName")
            .Options;

        MoviesApiContext context = new MoviesApiContext(options);

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
}
