using System;
using BlazorApp1.Models;
using BlazorWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services;

public class UserProfileService : IUserProfileService
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IDbContextFactory<BlazorWebAppContext> _dbContextFactory;
  public UserProfileService(IHttpContextAccessor httpContextAccessor, IDbContextFactory<BlazorWebAppContext> dbContextFactory)
  {
    _httpContextAccessor = httpContextAccessor;
    _dbContextFactory = dbContextFactory;
  }
  public async Task<UserProfile?> GetAsync()
  {
    var sub = _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;
    if (sub == null) return null;
    using var context = _dbContextFactory.CreateDbContext();
    UserProfile? UserProfile = await context.UserProfiles.FirstOrDefaultAsync(u => u.Sub == sub);
    if (UserProfile is null)
    {
      UserProfile = new UserProfile { Sub = sub };
      context.UserProfiles.Add(UserProfile);
      await context.SaveChangesAsync();
    }
    return UserProfile;
  }
}
