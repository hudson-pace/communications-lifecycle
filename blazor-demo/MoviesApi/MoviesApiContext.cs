using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
public class MoviesApiContext : DbContext
{
  public MoviesApiContext(DbContextOptions<MoviesApiContext> options) : base(options)
  {
  }
  public DbSet<Movie> Movies { get; set; }
  public DbSet<Communication> Communications { get; set; }
  public DbSet<CommunicationStatus> CommunicationStatuses { get; set; }
  public DbSet<CommunicationStatusChange> CommunicationStatusChanges { get; set; }
  public DbSet<CommunicationType> communicationTypes { get; set; }
}