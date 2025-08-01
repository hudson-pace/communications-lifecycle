using System;
using BlazorApp1.Models;
using BlazorWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services;

public class ForumCommentService : IForumCommentService
{
  private readonly IDbContextFactory<BlazorWebAppContext> _dbContextFactory;
  public ForumCommentService(IDbContextFactory<BlazorWebAppContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }
  public async Task<List<Comment>> GetCommentsFromPost(Post Post)
  {
    using var context = _dbContextFactory.CreateDbContext();
    List<Comment> comments = await context.Comments.Where(c => c.Post == Post).ToListAsync();
    return comments;
  }
  public async Task Create(Comment Comment)
  {
    using var context = _dbContextFactory.CreateDbContext();
    context.Comments.Add(Comment);
    await context.SaveChangesAsync();
  }
  public async Task Delete(Comment Comment)
  {
    using var context = _dbContextFactory.CreateDbContext();
    context.Comments.Remove(Comment);
    await context.SaveChangesAsync();
  }
}
