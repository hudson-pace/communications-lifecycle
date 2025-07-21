using NUnit.Framework;
using BlazorApp1;
using BlazorApp1.Services;
using BlazorWebApp.Data;
using BlazorApp1.Models;

namespace BlazorApp1.Tests;

[TestFixture]
public class Tests
{
    private ForumPostService _postService;
    [SetUp]
    public void Setup()
    {
        TestDbContextFactory testDbContextFactory = new TestDbContextFactory();
        BlazorWebAppContext context = testDbContextFactory.CreateDbContext();

        // test data
        context.Post.Add(new Post
        {
            Title = "Test",
            Content = "Test",
            Upvotes = 11,
            Downvotes = 2,
        });
        context.SaveChanges();

        _postService = new ForumPostService(new TestDbContextFactory());

    }

    [Test]
    public async Task Test1()
    {
        List<Post> posts = await _postService.GetAllAsync();
        Console.WriteLine(posts[0].Title);
        Assert.That(posts.Count(), Is.EqualTo(1));
    }
}
