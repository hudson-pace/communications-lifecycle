using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
public class MoviesApiContext : DbContext
{
  public MoviesApiContext(DbContextOptions<MoviesApiContext> options) : base(options)
  {
  }
  public DbSet<Movie> Movies  { get; set; }
}