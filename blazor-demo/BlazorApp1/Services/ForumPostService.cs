using System;
using BlazorApp1.Models;
using BlazorWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services;

public class ForumPostService : IForumPostService
{
  private readonly IDbContextFactory<BlazorWebAppContext> _dbContextFactory;
  public ForumPostService(IDbContextFactory<BlazorWebAppContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }
  public async Task<List<Post>> GetAllAsync()
  {
    using var context = _dbContextFactory.CreateDbContext();
    List<Post> posts = await context.Post.ToListAsync();
    return posts;
  }
  public async Task<Post?> GetOneAsync(int postId)
  {
    using var context = _dbContextFactory.CreateDbContext();
    Post? post = await context.Post.FirstOrDefaultAsync(p => p.Id == postId);
    return post;
  }
  public async Task Create(Post Post)
  {
    using var context = _dbContextFactory.CreateDbContext();
    context.Post.Add(Post);
    await context.SaveChangesAsync();
  }
  public async Task Delete(Post Post)
  {
    using var context = _dbContextFactory.CreateDbContext();
    context.Post.Remove(Post!);
    await context.SaveChangesAsync();
  }
}
