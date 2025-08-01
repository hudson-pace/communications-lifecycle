using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
public class CommLifecycleApiContext : DbContext
{
  public CommLifecycleApiContext(DbContextOptions<CommLifecycleApiContext> options) : base(options)
  {
  }
  public DbSet<Movie> Movies { get; set; }
  public DbSet<Communication> Communications { get; set; }
  public DbSet<CommunicationStatus> CommunicationStatuses { get; set; }
  public DbSet<CommunicationStatusChange> CommunicationStatusChanges { get; set; }
  public DbSet<CommunicationType> CommunicationTypes { get; set; }
}