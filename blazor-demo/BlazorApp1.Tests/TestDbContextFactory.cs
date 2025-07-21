using System;
using BlazorWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Tests;

public class TestDbContextFactory : IDbContextFactory<BlazorWebAppContext>
{
  private readonly DbContextOptions<BlazorWebAppContext> _options;
  public TestDbContextFactory(string dbName = "TestDb")
  {
    _options = new DbContextOptionsBuilder<BlazorWebAppContext>()
      .UseInMemoryDatabase(dbName)
      .Options;
  }
  public BlazorWebAppContext CreateDbContext()
  {
    return new BlazorWebAppContext(_options);
  }
}
