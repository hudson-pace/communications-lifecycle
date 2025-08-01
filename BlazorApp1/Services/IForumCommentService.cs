using System;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services;

public interface IForumCommentService
{
  Task<List<Comment>> GetCommentsFromPost(Post Post);
  Task Create(Comment Comment);
  Task Delete(Comment Comment);
}