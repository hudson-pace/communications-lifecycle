using System;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services;

public interface IForumPostService
{
  Task<List<Post>> GetAllAsync();
  Task<Post?> GetOneAsync(int postId);
  Task Create(Post Post);
  Task Delete(Post Post);
}
