using System;
using BlazorApp1.Models;

namespace BlazorApp1.Services;

public interface IUserProfileService
{
  Task<UserProfile?> GetAsync();
}