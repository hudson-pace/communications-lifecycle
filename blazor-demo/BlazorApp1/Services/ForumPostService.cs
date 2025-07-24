using System;
using BlazorApp1.DTO;
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
    List<Post> posts = await context.Posts
      .AsNoTracking()
      .Include(p => p.Author)
      .ToListAsync();
    return posts;
  }
  public async Task<Post?> GetOneAsync(int postId)
  {
    using var context = _dbContextFactory.CreateDbContext();
    Post? post = await context.Posts
      .AsNoTracking()
      .Include(p => p.PostTags)
      .ThenInclude(pt => pt.Tag)
      .FirstOrDefaultAsync(p => p.Id == postId);
    return post;
  }
  public async Task Create(PostCreateDto PostCreateDto)
  {
    using var context = _dbContextFactory.CreateDbContext();
    Post post = await GetPostFromPostCreateDto(PostCreateDto);
    context.Posts.Add(post);
    await context.SaveChangesAsync();
  }
  public async Task Delete(Post Post)
  {
    using var context = _dbContextFactory.CreateDbContext();
    context.Posts.Remove(Post!);
    await context.SaveChangesAsync();
  }

  private async Task<Post> GetPostFromPostCreateDto(PostCreateDto PostCreateDto)
  {
    using var context = _dbContextFactory.CreateDbContext();
    
    Post Post = new()
    {
      Title = PostCreateDto.Title,
      Content = PostCreateDto.Content,
      PostTags = new List<PostTag>(),
      AuthorId = PostCreateDto.AuthorId,
    };

    foreach (string postTag in PostCreateDto.PostTags)
    {
      Tag? Tag = await context.Tags.FirstOrDefaultAsync(t => t.Name == postTag);
      Tag ??= new Tag
        {
          Name = postTag
        };
      PostTag PostTag = new PostTag
      {
        Post = Post,
        Tag = Tag,
      };
      Post.PostTags.Add(PostTag);
    };
    return Post;
  }
}
